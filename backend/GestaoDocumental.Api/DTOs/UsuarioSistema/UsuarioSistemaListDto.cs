namespace GestaoDocumental.Api.DTOs.UsuarioSistema;

public class UsuarioSistemaListDto
{
    public int Id { get; set; }

    public int ColaboradorId { get; set; }

    public string Username { get; set; } = string.Empty;

    public int EstadoLoginId { get; set; }

    public bool? Ativo { get; set; }

    public bool? Bloqueado { get; set; }
}
