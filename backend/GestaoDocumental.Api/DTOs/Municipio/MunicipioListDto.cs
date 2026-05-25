namespace GestaoDocumental.Api.DTOs.Municipio;

public class MunicipioListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int ProvinciaId { get; set; }
}
