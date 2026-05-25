namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoDownloadReportDto
{
    public int DocumentoId { get; set; }

    public int TotalDownloads { get; set; }

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public IReadOnlyList<DocumentoDownloadReportItemDto> Downloads { get; set; } = [];
}
