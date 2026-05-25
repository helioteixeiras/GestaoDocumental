namespace GestaoDocumental.Api.DTOs.DocumentoHistorico;

public class DocumentoHistoricoUpdateDto
{
    public string Acao { get; set; } = string.Empty;

    public string? Observacao { get; set; }
}
