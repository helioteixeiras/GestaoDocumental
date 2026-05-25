namespace GestaoDocumental.Api.DTOs.Departamento;

public class DepartamentoCreateDto
{
    public string Nome { get; set; } = string.Empty;

    public string? Sigla { get; set; }

    public int DirecaoId { get; set; }
}
