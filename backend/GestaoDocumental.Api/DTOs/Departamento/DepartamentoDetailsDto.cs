namespace GestaoDocumental.Api.DTOs.Departamento;

public class DepartamentoDetailsDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Sigla { get; set; }

    public int DirecaoId { get; set; }
}
