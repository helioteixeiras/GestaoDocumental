namespace GestaoDocumental.Api.DTOs.Pais;

public class PaisUpdateDto
{
    public string Nome { get; set; } = string.Empty;

    public string? SiglaISO2 { get; set; }

    public string? CodigoIso3 { get; set; }

    public string? Capital { get; set; }

    public string? IndicativoTelefonico { get; set; }
}
