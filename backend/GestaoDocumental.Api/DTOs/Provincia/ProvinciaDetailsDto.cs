namespace GestaoDocumental.Api.DTOs.Provincia;

public class ProvinciaDetailsDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int PaisId { get; set; }
}
