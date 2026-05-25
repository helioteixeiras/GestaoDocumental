namespace GestaoDocumental.Application.DTOs.TramitacaoDocumento;

public class EncaminharDocumentoDto
{
    public int DirecaoDestinoId { get; set; }

    public int? ColaboradorDestinoId { get; set; }

    public string? Observacao { get; set; }
}
