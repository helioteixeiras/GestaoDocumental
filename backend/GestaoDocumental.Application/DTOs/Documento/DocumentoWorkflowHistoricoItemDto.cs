namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoWorkflowHistoricoItemDto
{
    public int Id { get; set; }

    public DateTime DataAcao { get; set; }

    public string Acao { get; set; } = string.Empty;

    public string? Observacao { get; set; }

    public string? Utilizador { get; set; }
}
