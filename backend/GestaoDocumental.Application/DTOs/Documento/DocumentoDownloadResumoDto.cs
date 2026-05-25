namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoDownloadResumoDto
{
    public int DocumentoId { get; set; }

    public int TotalDownloads { get; set; }

    public DateTime? PrimeiroDownload { get; set; }

    public DateTime? UltimoDownload { get; set; }

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public IReadOnlyList<DownloadResumoPorAcaoDto> DownloadsPorAcao { get; set; } = [];

    public IReadOnlyList<DownloadResumoPorUsuarioDto> DownloadsPorUsuario { get; set; } = [];

    public IReadOnlyList<DownloadResumoPorDiaDto> DownloadsPorDia { get; set; } = [];

    public IReadOnlyList<DownloadResumoPorFicheiroDto> FicheirosMaisBaixados { get; set; } = [];
}
