namespace GestaoDocumental.Api.DTOs.Municipio;

public class MunicipioUpdateDto
{
    public string Nome { get; set; } = string.Empty;

    public int ProvinciaId { get; set; }
}
