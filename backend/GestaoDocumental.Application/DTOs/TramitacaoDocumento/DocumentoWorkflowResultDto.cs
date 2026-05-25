namespace GestaoDocumental.Application.DTOs.TramitacaoDocumento;

public class DocumentoWorkflowResultDto
{
    public int DocumentoId { get; set; }

    public string EstadoDocumento { get; set; } = string.Empty;

    public int TramitacaoId { get; set; }

    public string Acao { get; set; } = string.Empty;

    public DateTime DataAcao { get; set; }

    public string? Observacao { get; set; }
}
