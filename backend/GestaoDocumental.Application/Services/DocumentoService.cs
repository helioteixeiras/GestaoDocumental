using GestaoDocumental.Application.Common;
using GestaoDocumental.Application.DTOs.Documento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Shared.Settings;
using Microsoft.Extensions.Options;

namespace GestaoDocumental.Application.Services;

public class DocumentoService
    : GenericService<Documento>,
      IDocumentoService
{
    private const string UploadHistoricoAcao = "UploadArquivo";

    private readonly IDocumentoWorkflowRepository _workflowRepository;
    private readonly IDocumentoAnexoRepository _anexoRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly StorageSettings _storageSettings;

    public DocumentoService(
        IGenericRepository<Documento> repository,
        IUnitOfWork unitOfWork,
        IDocumentoWorkflowRepository workflowRepository,
        IDocumentoAnexoRepository anexoRepository,
        IFileStorageService fileStorageService,
        IOptions<StorageSettings> storageSettings)
        : base(repository, unitOfWork)
    {
        _workflowRepository = workflowRepository;
        _anexoRepository = anexoRepository;
        _fileStorageService = fileStorageService;
        _storageSettings = storageSettings.Value;
    }

    public async Task<DocumentoUploadResultDto> UploadArquivoAsync(
        int documentoId,
        int usuarioSistemaId,
        string fileName,
        long fileLength,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        DocumentoFileValidator.Validate(fileName, fileLength, _storageSettings);

        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var usuario = await _workflowRepository.GetUsuarioAsync(usuarioSistemaId, cancellationToken);
        if (usuario is null || !usuario.Ativo || usuario.Bloqueado)
        {
            throw new UnauthorizedAccessException("Utilizador autenticado inválido.");
        }

        var storedFile = await _fileStorageService.SaveAsync(documentoId, fileName, content, cancellationToken);
        var agora = DateTime.UtcNow;
        var guidFicheiro = Guid.NewGuid();
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        var anexo = new DocumentoAnexo
        {
            DocumentoId = documentoId,
            GuidFicheiro = guidFicheiro,
            NomeOriginal = Path.GetFileName(fileName),
            NomeFisico = storedFile.PhysicalFileName,
            Extensao = extension,
            Caminho = storedFile.RelativePath,
            HashSha256 = storedFile.HashSha256,
            Tamanho = storedFile.Size,
            DataUpload = agora,
            Ativo = true,
            DataCriacao = agora
        };

        await _anexoRepository.AddAsync(anexo);

        documento.DataAtualizacao = agora;
        Repository.Update(documento);

        await _workflowRepository.AddHistoricoAsync(
            new DocumentoHistorico
            {
                DocumentoId = documentoId,
                UtilizadorId = usuario.ColaboradorId ?? documento.ColaboradorCriadorId,
                Acao = UploadHistoricoAcao,
                Observacao = anexo.NomeOriginal,
                DataAcao = agora,
                Ativo = true,
                DataCriacao = agora
            },
            cancellationToken);

        await UnitOfWork.SaveChangesAsync();

        var versao = await _anexoRepository.CountByDocumentoIdAsync(documentoId, cancellationToken);

        return new DocumentoUploadResultDto
        {
            DocumentoId = documentoId,
            AnexoId = anexo.Id,
            GuidFicheiro = guidFicheiro,
            NomeOriginal = anexo.NomeOriginal,
            Extensao = extension,
            Tamanho = storedFile.Size,
            DataUpload = agora,
            Versao = versao
        };
    }

    public async Task<DocumentoDownloadResultDto?> DownloadArquivoAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            return null;
        }

        var anexo = await _anexoRepository.GetLatestByDocumentoIdAsync(documentoId, cancellationToken);
        if (anexo is null || string.IsNullOrWhiteSpace(anexo.Caminho))
        {
            return null;
        }

        var fileContent = await _fileStorageService.OpenReadAsync(
            anexo.Caminho,
            anexo.NomeOriginal,
            cancellationToken);

        if (fileContent is null)
        {
            return null;
        }

        return new DocumentoDownloadResultDto
        {
            FileName = fileContent.FileName,
            ContentType = fileContent.ContentType,
            Size = fileContent.Size,
            Content = fileContent.Content
        };
    }

    public async Task<DocumentoWorkflowTimelineDto> ObterWorkflowDocumentoAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        var workflow = await _workflowRepository.GetWorkflowByDocumentoIdAsync(documentoId, cancellationToken);

        if (workflow is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var listaHistorico = workflow.Historicos
            .Select(historico => new DocumentoWorkflowHistoricoItemDto
            {
                Id = historico.Id,
                DataAcao = historico.DataAcao,
                Acao = historico.Acao,
                Observacao = historico.Observacao,
                Utilizador = historico.Utilizador?.Nome
            })
            .ToList();

        var listaTramitacoes = workflow.Tramitacoes
            .Select(tramitacao => new DocumentoWorkflowTramitacaoItemDto
            {
                Id = tramitacao.Id,
                DataEnvio = tramitacao.DataEnvio,
                DataRececao = tramitacao.DataRececao,
                Estado = tramitacao.Estado,
                Observacao = tramitacao.Observacao,
                ColaboradorOrigem = tramitacao.ColaboradorOrigem?.Nome,
                ColaboradorDestino = tramitacao.ColaboradorDestino?.Nome,
                DirecaoOrigem = tramitacao.DirecaoOrigem?.Nome,
                DirecaoDestino = tramitacao.DirecaoDestino?.Nome
            })
            .ToList();

        var timeline = workflow.Historicos
            .Select(historico => new DocumentoWorkflowTimelineItemDto
            {
                Data = historico.DataAcao,
                TipoEvento = "Historico",
                Acao = historico.Acao,
                Observacao = historico.Observacao,
                Utilizador = historico.Utilizador?.Nome,
                Estado = null
            })
            .Concat(workflow.Tramitacoes.Select(tramitacao => new DocumentoWorkflowTimelineItemDto
            {
                Data = tramitacao.DataEnvio,
                TipoEvento = "Tramitacao",
                Acao = string.IsNullOrWhiteSpace(tramitacao.Estado) ? "Tramitacao" : tramitacao.Estado,
                Observacao = tramitacao.Observacao,
                Utilizador = tramitacao.ColaboradorOrigem?.Nome ?? tramitacao.ColaboradorDestino?.Nome,
                Estado = tramitacao.Estado
            }))
            .OrderBy(item => item.Data)
            .ThenBy(item => item.TipoEvento)
            .ToList();

        var ultimaAcao = timeline.LastOrDefault()?.Acao;

        return new DocumentoWorkflowTimelineDto
        {
            Resumo = new DocumentoWorkflowResumoDto
            {
                DocumentoId = workflow.Documento.Id,
                NumeroDocumento = workflow.Documento.NumeroDocumento,
                Titulo = workflow.Documento.Titulo,
                EstadoAtual = workflow.Documento.EstadoDocumento.Nome,
                DataCriacao = workflow.Documento.DataCriacao,
                TotalTramitacoes = listaTramitacoes.Count,
                TotalHistorico = listaHistorico.Count,
                UltimaAcao = ultimaAcao
            },
            ListaHistorico = listaHistorico,
            ListaTramitacoes = listaTramitacoes,
            Timeline = timeline
        };
    }
}
