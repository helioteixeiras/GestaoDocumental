using GestaoDocumental.Application.Common;
using GestaoDocumental.Application.DTOs.Documento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Application.Models;
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
    private const string DownloadHistoricoAcao = "DownloadArquivo";
    private const string DownloadVersaoHistoricoAcao = "DownloadArquivoVersao";
    private const string RemocaoAnexoHistoricoAcao = "RemocaoAnexo";
    private const int DefaultDownloadReportPage = 1;
    private const int DefaultDownloadReportPageSize = 20;
    private const int MaxDownloadReportPageSize = 100;

    private readonly IDocumentoWorkflowRepository _workflowRepository;
    private readonly IDocumentoAnexoRepository _anexoRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly ICsvExportService _csvExportService;
    private readonly StorageSettings _storageSettings;

    public DocumentoService(
        IGenericRepository<Documento> repository,
        IUnitOfWork unitOfWork,
        IDocumentoWorkflowRepository workflowRepository,
        IDocumentoAnexoRepository anexoRepository,
        IFileStorageService fileStorageService,
        ICsvExportService csvExportService,
        IOptions<StorageSettings> storageSettings)
        : base(repository, unitOfWork)
    {
        _workflowRepository = workflowRepository;
        _anexoRepository = anexoRepository;
        _fileStorageService = fileStorageService;
        _csvExportService = csvExportService;
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
        int usuarioSistemaId,
        CancellationToken cancellationToken = default)
    {
        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            return null;
        }

        var anexo = await _anexoRepository.GetLatestByDocumentoIdAsync(documentoId, cancellationToken);
        return await DownloadAnexoComAuditoriaAsync(
            documento,
            anexo,
            usuarioSistemaId,
            DownloadHistoricoAcao,
            cancellationToken);
    }

    public async Task<DocumentoAnexoListDto> ListarAnexosAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var anexos = await _anexoRepository.GetByDocumentoIdAsync(documentoId, cancellationToken);
        if (anexos.Count == 0)
        {
            return new DocumentoAnexoListDto
            {
                DocumentoId = documentoId,
                TotalAnexos = 0,
                UltimaVersao = null,
                Anexos = []
            };
        }

        var versoesPorId = anexos
            .OrderBy(anexo => anexo.DataUpload)
            .ThenBy(anexo => anexo.Id)
            .Select((anexo, index) => (anexo.Id, Versao: index + 1))
            .ToDictionary(item => item.Id, item => item.Versao);

        var ultimoAnexoId = anexos[0].Id;
        var ultimaVersao = versoesPorId[ultimoAnexoId];

        var itens = anexos.Select(anexo => new DocumentoAnexoListItemDto
        {
            Id = anexo.Id,
            DocumentoId = anexo.DocumentoId,
            NomeOriginal = anexo.NomeOriginal,
            Extensao = anexo.Extensao,
            Tamanho = anexo.Tamanho ?? 0,
            TamanhoFormatado = FileSizeFormatter.Format(anexo.Tamanho ?? 0),
            HashSha256 = anexo.HashSha256,
            DataUpload = anexo.DataUpload,
            Versao = versoesPorId[anexo.Id],
            EhUltimaVersao = anexo.Id == ultimoAnexoId,
            DownloadUrl = $"/api/Documento/{documentoId}/anexos/{anexo.Id}/download"
        }).ToList();

        return new DocumentoAnexoListDto
        {
            DocumentoId = documentoId,
            TotalAnexos = itens.Count,
            UltimaVersao = ultimaVersao,
            Anexos = itens
        };
    }

    public async Task<DocumentoDownloadResultDto?> DownloadAnexoAsync(
        int documentoId,
        int anexoId,
        int usuarioSistemaId,
        CancellationToken cancellationToken = default)
    {
        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            return null;
        }

        var anexo = await _anexoRepository.GetByDocumentoIdAndAnexoIdAsync(
            documentoId,
            anexoId,
            cancellationToken);

        return await DownloadAnexoComAuditoriaAsync(
            documento,
            anexo,
            usuarioSistemaId,
            DownloadVersaoHistoricoAcao,
            cancellationToken);
    }

    public async Task<bool> RemoverAnexoAsync(
        int documentoId,
        int anexoId,
        int usuarioSistemaId,
        CancellationToken cancellationToken = default)
    {
        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var anexo = await _anexoRepository.GetActiveByDocumentoIdAndAnexoIdAsync(
            documentoId,
            anexoId,
            cancellationToken);

        if (anexo is null)
        {
            return false;
        }

        var versao = await ObterVersaoAnexoAsync(documentoId, anexoId, cancellationToken);
        var agora = DateTime.UtcNow;

        await _anexoRepository.SoftDeleteAsync(anexo, cancellationToken);

        documento.DataAtualizacao = agora;
        Repository.Update(documento);

        await RegistrarHistoricoDocumentoAsync(
            documento,
            usuarioSistemaId,
            RemocaoAnexoHistoricoAcao,
            BuildObservacaoAnexo(anexo, versao),
            cancellationToken);

        return true;
    }

    private async Task<DocumentoDownloadResultDto?> DownloadAnexoComAuditoriaAsync(
        Documento documento,
        DocumentoAnexo? anexo,
        int usuarioSistemaId,
        string acao,
        CancellationToken cancellationToken)
    {
        var result = await OpenAnexoDownloadAsync(anexo, cancellationToken);
        if (result is null || anexo is null)
        {
            return null;
        }

        var versao = await ObterVersaoAnexoAsync(documento.Id, anexo.Id, cancellationToken);
        var observacao = BuildObservacaoAnexo(anexo, versao);

        await RegistrarHistoricoDocumentoAsync(
            documento,
            usuarioSistemaId,
            acao,
            observacao,
            cancellationToken);

        return result;
    }

    private async Task RegistrarHistoricoDocumentoAsync(
        Documento documento,
        int usuarioSistemaId,
        string acao,
        string observacao,
        CancellationToken cancellationToken)
    {
        var usuario = await _workflowRepository.GetUsuarioAsync(usuarioSistemaId, cancellationToken);
        if (usuario is null || !usuario.Ativo || usuario.Bloqueado)
        {
            throw new UnauthorizedAccessException("Utilizador autenticado inválido.");
        }

        var agora = DateTime.UtcNow;

        await _workflowRepository.AddHistoricoAsync(
            new DocumentoHistorico
            {
                DocumentoId = documento.Id,
                UtilizadorId = usuario.ColaboradorId ?? documento.ColaboradorCriadorId,
                Acao = acao,
                Observacao = observacao,
                DataAcao = agora,
                Ativo = true,
                DataCriacao = agora
            },
            cancellationToken);

        await UnitOfWork.SaveChangesAsync();
    }

    private static string BuildObservacaoAnexo(DocumentoAnexo anexo, int? versao)
    {
        var partes = new List<string> { anexo.NomeOriginal, $"AnexoId={anexo.Id}" };

        if (versao.HasValue)
        {
            partes.Add($"Versao={versao.Value}");
        }

        if (!string.IsNullOrWhiteSpace(anexo.HashSha256))
        {
            partes.Add($"HashSha256={anexo.HashSha256}");
        }

        return string.Join(" | ", partes);
    }

    private async Task<int?> ObterVersaoAnexoAsync(
        int documentoId,
        int anexoId,
        CancellationToken cancellationToken)
    {
        var anexos = await _anexoRepository.GetByDocumentoIdAsync(documentoId, cancellationToken);
        var indice = anexos
            .OrderBy(anexo => anexo.DataUpload)
            .ThenBy(anexo => anexo.Id)
            .Select((anexo, index) => new { anexo.Id, Versao = index + 1 })
            .FirstOrDefault(item => item.Id == anexoId);

        return indice?.Versao;
    }

    private async Task<DocumentoDownloadResultDto?> OpenAnexoDownloadAsync(
        DocumentoAnexo? anexo,
        CancellationToken cancellationToken)
    {
        if (anexo is null || !anexo.Ativo || string.IsNullOrWhiteSpace(anexo.Caminho))
        {
            return null;
        }

        if (!await _fileStorageService.ExistsAsync(anexo.Caminho, cancellationToken))
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

    public async Task<DocumentoDownloadReportDto> ObterRelatorioDownloadsAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        int? usuarioId,
        string? acao,
        int? page = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        if (dataInicio.HasValue && dataFim.HasValue && dataInicio.Value > dataFim.Value)
        {
            throw new InvalidOperationException("dataInicio não pode ser maior que dataFim.");
        }

        var pagina = page ?? DefaultDownloadReportPage;
        var tamanhoPagina = pageSize ?? DefaultDownloadReportPageSize;
        ValidarPaginacaoRelatorioDownloads(pagina, tamanhoPagina);

        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var totalDownloads = await _workflowRepository.CountDownloadHistoricoByDocumentoIdAsync(
            documentoId,
            dataInicio,
            dataFim,
            usuarioId,
            acao,
            cancellationToken);

        var totalPages = totalDownloads == 0
            ? 0
            : (int)Math.Ceiling(totalDownloads / (double)tamanhoPagina);

        var historicos = totalDownloads == 0
            ? Array.Empty<DocumentoHistorico>()
            : await _workflowRepository.GetDownloadHistoricoByDocumentoIdPagedAsync(
                documentoId,
                dataInicio,
                dataFim,
                usuarioId,
                acao,
                pagina,
                tamanhoPagina,
                cancellationToken);

        var downloads = historicos
            .Select(MapDownloadHistoricoItem)
            .ToList();

        return new DocumentoDownloadReportDto
        {
            DocumentoId = documentoId,
            TotalDownloads = totalDownloads,
            DataInicio = dataInicio,
            DataFim = dataFim,
            Page = pagina,
            PageSize = tamanhoPagina,
            TotalPages = totalPages,
            HasNextPage = totalPages > 0 && pagina < totalPages,
            HasPreviousPage = pagina > 1 && totalPages > 0,
            Downloads = downloads
        };
    }

    private static void ValidarPaginacaoRelatorioDownloads(int page, int pageSize)
    {
        if (page < 1)
        {
            throw new InvalidOperationException("page deve ser maior ou igual a 1.");
        }

        if (pageSize < 1 || pageSize > MaxDownloadReportPageSize)
        {
            throw new InvalidOperationException($"pageSize deve estar entre 1 e {MaxDownloadReportPageSize}.");
        }
    }

    public async Task<FileExportResultDto> ExportarRelatorioDownloadsCsvAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        int? usuarioId,
        string? acao,
        CancellationToken cancellationToken = default)
    {
        if (dataInicio.HasValue && dataFim.HasValue && dataInicio.Value > dataFim.Value)
        {
            throw new InvalidOperationException("dataInicio não pode ser maior que dataFim.");
        }

        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var historicos = await _workflowRepository.GetDownloadHistoricoByDocumentoIdAsync(
            documentoId,
            dataInicio,
            dataFim,
            usuarioId,
            acao,
            cancellationToken);

        var headers = new[]
        {
            "HistoricoId",
            "DataAcao",
            "Acao",
            "UsuarioId",
            "UsuarioNome",
            "AnexoId",
            "Versao",
            "HashSha256",
            "Observacao"
        };

        var rows = historicos
            .Select(historico =>
            {
                var item = MapDownloadHistoricoItem(historico);
                return (IReadOnlyList<string?>)
                [
                    item.HistoricoId.ToString(),
                    item.DataAcao.ToString("O"),
                    item.Acao,
                    item.UsuarioId.ToString(),
                    item.UsuarioNome,
                    item.AnexoId?.ToString(),
                    item.Versao?.ToString(),
                    item.HashSha256,
                    item.Observacao
                ];
            })
            .ToList();

        var content = _csvExportService.BuildCsv(headers, rows);

        return new FileExportResultDto
        {
            Content = content,
            FileName = $"downloads-documento-{documentoId}-{DateTime.UtcNow:yyyyMMddHHmmss}.csv",
            ContentType = "text/csv; charset=utf-8"
        };
    }

    private static DocumentoDownloadReportItemDto MapDownloadHistoricoItem(DocumentoHistorico historico) =>
        new()
        {
            HistoricoId = historico.Id,
            DataAcao = historico.DataAcao,
            Acao = historico.Acao,
            Observacao = historico.Observacao,
            UsuarioId = historico.UtilizadorId,
            UsuarioNome = historico.Utilizador?.Nome,
            AnexoId = DownloadHistoricoObservacaoParser.ExtractAnexoId(historico.Observacao),
            Versao = DownloadHistoricoObservacaoParser.ExtractVersao(historico.Observacao),
            HashSha256 = DownloadHistoricoObservacaoParser.ExtractHashSha256(historico.Observacao)
        };

    public async Task<DocumentoDownloadResumoDto> ObterResumoDownloadsAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        CancellationToken cancellationToken = default)
    {
        if (dataInicio.HasValue && dataFim.HasValue && dataInicio.Value > dataFim.Value)
        {
            throw new InvalidOperationException("dataInicio não pode ser maior que dataFim.");
        }

        var documento = await Repository.GetByIdAsync(documentoId);
        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var historicos = await _workflowRepository.GetDownloadHistoricoByDocumentoIdAsync(
            documentoId,
            dataInicio,
            dataFim,
            usuarioId: null,
            acao: null,
            cancellationToken);

        if (historicos.Count == 0)
        {
            return new DocumentoDownloadResumoDto
            {
                DocumentoId = documentoId,
                TotalDownloads = 0,
                DataInicio = dataInicio,
                DataFim = dataFim
            };
        }

        var downloadsPorAcao = historicos
            .GroupBy(historico => historico.Acao)
            .Select(grupo => new DownloadResumoPorAcaoDto
            {
                Acao = grupo.Key,
                Total = grupo.Count()
            })
            .OrderByDescending(item => item.Total)
            .ThenBy(item => item.Acao)
            .ToList();

        var downloadsPorUsuario = historicos
            .GroupBy(historico => new
            {
                historico.UtilizadorId,
                UsuarioNome = historico.Utilizador?.Nome
            })
            .Select(grupo => new DownloadResumoPorUsuarioDto
            {
                UsuarioId = grupo.Key.UtilizadorId,
                UsuarioNome = grupo.Key.UsuarioNome,
                Total = grupo.Count()
            })
            .OrderByDescending(item => item.Total)
            .ThenBy(item => item.UsuarioNome)
            .ToList();

        var downloadsPorDia = historicos
            .GroupBy(historico => historico.DataAcao.Date)
            .Select(grupo => new DownloadResumoPorDiaDto
            {
                Data = grupo.Key,
                Total = grupo.Count()
            })
            .OrderBy(item => item.Data)
            .ToList();

        var ficheirosMaisBaixados = historicos
            .Select(historico => new
            {
                NomeOriginal = DownloadHistoricoObservacaoParser.ExtractNomeOriginal(historico.Observacao),
                AnexoId = DownloadHistoricoObservacaoParser.ExtractAnexoId(historico.Observacao),
                Versao = DownloadHistoricoObservacaoParser.ExtractVersao(historico.Observacao)
            })
            .GroupBy(item => new
            {
                item.NomeOriginal,
                item.AnexoId,
                item.Versao
            })
            .Select(grupo => new DownloadResumoPorFicheiroDto
            {
                NomeOriginal = grupo.Key.NomeOriginal,
                AnexoId = grupo.Key.AnexoId,
                Versao = grupo.Key.Versao,
                Total = grupo.Count()
            })
            .OrderByDescending(item => item.Total)
            .ThenByDescending(item => item.Versao)
            .ThenBy(item => item.NomeOriginal)
            .ToList();

        return new DocumentoDownloadResumoDto
        {
            DocumentoId = documentoId,
            TotalDownloads = historicos.Count,
            PrimeiroDownload = historicos.Min(historico => historico.DataAcao),
            UltimoDownload = historicos.Max(historico => historico.DataAcao),
            DataInicio = dataInicio,
            DataFim = dataFim,
            DownloadsPorAcao = downloadsPorAcao,
            DownloadsPorUsuario = downloadsPorUsuario,
            DownloadsPorDia = downloadsPorDia,
            FicheirosMaisBaixados = ficheirosMaisBaixados
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
