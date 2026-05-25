namespace GestaoDocumental.Application.DTOs.Documento;

public class DownloadResumoPorUsuarioDto
{
    public int UsuarioId { get; set; }

    public string? UsuarioNome { get; set; }

    public int Total { get; set; }
}
