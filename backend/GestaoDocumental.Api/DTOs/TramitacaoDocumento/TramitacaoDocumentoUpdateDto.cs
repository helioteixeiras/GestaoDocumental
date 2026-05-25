namespace GestaoDocumental.Api.DTOs.TramitacaoDocumento;

public class TramitacaoDocumentoUpdateDto
{
    public int DirecaoDestinoId { get; set; }

    public int? ColaboradorDestinoId { get; set; }

    public string? Estado { get; set; }

    public string? Observacao { get; set; }

    public DateTime? DataRececao { get; set; }
}
