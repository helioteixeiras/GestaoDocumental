namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoDownloadReportDto
{
    public int DocumentoId { get; set; }

    public int TotalDownloads { get; set; }

    public DateTime? DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }

    public IReadOnlyList<DocumentoDownloadReportItemDto> Downloads { get; set; } = [];
}
