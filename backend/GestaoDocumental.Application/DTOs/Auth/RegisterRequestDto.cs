namespace GestaoDocumental.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int PerfilId { get; set; }

    public int EstadoLoginId { get; set; }

    public int? ColaboradorId { get; set; }
}
