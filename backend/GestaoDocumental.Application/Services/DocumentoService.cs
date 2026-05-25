using GestaoDocumental.Application.DTOs.Documento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class DocumentoService
    : GenericService<Documento>,
      IDocumentoService
{
    private readonly IDocumentoWorkflowRepository _workflowRepository;

    public DocumentoService(
        IGenericRepository<Documento> repository,
        IUnitOfWork unitOfWork,
        IDocumentoWorkflowRepository workflowRepository)
        : base(repository, unitOfWork)
    {
        _workflowRepository = workflowRepository;
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
