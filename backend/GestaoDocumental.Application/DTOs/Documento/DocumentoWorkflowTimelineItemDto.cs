namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoWorkflowTimelineItemDto
{
    public DateTime Data { get; set; }

    public string TipoEvento { get; set; } = string.Empty;

    public string Acao { get; set; } = string.Empty;

    public string? Observacao { get; set; }

    public string? Utilizador { get; set; }

    public string? Estado { get; set; }
}
