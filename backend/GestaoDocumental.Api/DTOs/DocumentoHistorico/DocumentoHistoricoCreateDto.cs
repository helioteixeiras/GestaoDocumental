namespace GestaoDocumental.Api.DTOs.DocumentoHistorico;

public class DocumentoHistoricoCreateDto
{
    public int DocumentoId { get; set; }

    public string Acao { get; set; } = string.Empty;

    public string? Observacao { get; set; }
}
