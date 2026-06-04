namespace GestaoDocumental.Api.DTOs.Provincia;

public class ProvinciaCreateDto
{
    public string Nome { get; set; } = string.Empty;

    public int PaisId { get; set; }

    public string? Sigla { get; set; }

    public string? CodigoINE { get; set; }
}
