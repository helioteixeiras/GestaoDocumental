namespace GestaoDocumental.Api.DTOs.UsuarioSistema;

public class UsuarioSistemaCreateDto
{
    public int ColaboradorId { get; set; }

    public string Username { get; set; } = string.Empty;

    public int EstadoLoginId { get; set; }

    public bool? Ativo { get; set; }
}
