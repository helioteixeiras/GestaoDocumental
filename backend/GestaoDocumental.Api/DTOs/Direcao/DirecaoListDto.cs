namespace GestaoDocumental.Api.DTOs.Direcao;

public class DirecaoListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Sigla { get; set; }
}
