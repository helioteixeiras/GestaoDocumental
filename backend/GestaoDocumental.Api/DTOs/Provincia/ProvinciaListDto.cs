namespace GestaoDocumental.Api.DTOs.Provincia;

public class ProvinciaListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int PaisId { get; set; }
}
