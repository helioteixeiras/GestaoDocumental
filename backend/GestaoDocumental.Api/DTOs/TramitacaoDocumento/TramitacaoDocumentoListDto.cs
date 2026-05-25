namespace GestaoDocumental.Api.DTOs.TramitacaoDocumento;

public class TramitacaoDocumentoListDto
{
    public int Id { get; set; }

    public int DocumentoId { get; set; }

    public int DirecaoOrigemId { get; set; }

    public int DirecaoDestinoId { get; set; }

    public string? Estado { get; set; }

    public DateTime DataEnvio { get; set; }

    public DateTime? DataRececao { get; set; }
}
