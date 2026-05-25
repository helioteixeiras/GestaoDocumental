namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoWorkflowTimelineDto
{
    public DocumentoWorkflowResumoDto Resumo { get; set; } = new();

    public IReadOnlyList<DocumentoWorkflowHistoricoItemDto> ListaHistorico { get; set; } = [];

    public IReadOnlyList<DocumentoWorkflowTramitacaoItemDto> ListaTramitacoes { get; set; } = [];

    public IReadOnlyList<DocumentoWorkflowTimelineItemDto> Timeline { get; set; } = [];
}
