namespace GestaoDocumental.Application.DTOs.Documento;

public class DownloadResumoPorFicheiroDto
{
    public string? NomeOriginal { get; set; }

    public int? AnexoId { get; set; }

    public int? Versao { get; set; }

    public int Total { get; set; }
}
