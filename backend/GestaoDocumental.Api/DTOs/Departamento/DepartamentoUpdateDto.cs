namespace GestaoDocumental.Api.DTOs.Departamento;

public class DepartamentoUpdateDto
{
    public string Nome { get; set; } = string.Empty;

    public string? Sigla { get; set; }

    public int DirecaoId { get; set; }
}
