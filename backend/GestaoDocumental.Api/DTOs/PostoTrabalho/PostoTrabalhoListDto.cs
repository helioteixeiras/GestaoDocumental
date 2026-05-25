namespace GestaoDocumental.Api.DTOs.PostoTrabalho;

public class PostoTrabalhoListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Sigla { get; set; }

    public int DepartamentoId { get; set; }

    public int MunicipioId { get; set; }
}
