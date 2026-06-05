namespace GestaoDocumental.Api.DTOs.TipoDocumento;

public class TipoDocumentoUpdateDto
{
    public string Codigo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int CategoriaDocumentoId { get; set; }

    public bool Ativo { get; set; }
}
