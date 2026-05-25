namespace GestaoDocumental.Api.DTOs.DocumentoHistorico;

public class DocumentoHistoricoDetailsDto
{
    public int Id { get; set; }

    public int DocumentoId { get; set; }

    public int UtilizadorId { get; set; }

    public string Acao { get; set; } = string.Empty;

    public string? Observacao { get; set; }

    public DateTime DataAcao { get; set; }
}
