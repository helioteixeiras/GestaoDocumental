namespace GestaoDocumental.Api.DTOs.TramitacaoDocumento;

public class TramitacaoDocumentoCreateDto
{
    public int DocumentoId { get; set; }

    public int DirecaoOrigemId { get; set; }

    public int DirecaoDestinoId { get; set; }

    public int? ColaboradorOrigemId { get; set; }

    public int? ColaboradorDestinoId { get; set; }

    public string? Estado { get; set; }

    public string? Observacao { get; set; }
}
