namespace GestaoDocumental.Api.DTOs.TipoDocumento;

public class TipoDocumentoListDto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int CategoriaDocumentoId { get; set; }
}
