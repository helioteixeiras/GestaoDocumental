namespace GestaoDocumental.Application.DTOs.Documento;

public sealed class DocumentoDownloadResultDto : IDisposable
{
    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = "application/octet-stream";

    public long Size { get; set; }

    public Stream Content { get; set; } = Stream.Null;

    public void Dispose()
    {
        Content.Dispose();
    }
}
