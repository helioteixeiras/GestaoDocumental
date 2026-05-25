namespace GestaoDocumental.Api.DTOs.UsuarioSistema;

public class UsuarioSistemaUpdateDto
{
    public string Username { get; set; } = string.Empty;

    public int EstadoLoginId { get; set; }

    public bool? Ativo { get; set; }

    public bool? Bloqueado { get; set; }
}
