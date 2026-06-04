namespace GestaoDocumental.Api.DTOs.Pais;

public class PaisDetailsDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? SiglaISO2 { get; set; }

    public string? CodigoIso3 { get; set; }

    public string? Capital { get; set; }

    public string? IndicativoTelefonico { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}
