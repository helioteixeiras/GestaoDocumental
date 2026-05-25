namespace GestaoDocumental.Api.DTOs.Colaborador;

public class ColaboradorListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string NumDocumento { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Cargo { get; set; }

    public int EstadoId { get; set; }

    public int PerfilId { get; set; }

    public int PostoTrabalhoId { get; set; }
}
