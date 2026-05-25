namespace GestaoDocumental.Api.DTOs.Municipio;

public class MunicipioDetailsDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int ProvinciaId { get; set; }
}
