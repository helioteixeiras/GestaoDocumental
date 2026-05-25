namespace GestaoDocumental.Api.DTOs.Provincia;

public class ProvinciaCreateDto
{
    public string Nome { get; set; } = string.Empty;

    public int PaisId { get; set; }
}
