namespace GestaoDocumental.Api.DTOs.TipoDocumento;

public class TipoDocumentoCreateDto
{
    public string Codigo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int CategoriaDocumentoId { get; set; }
}
