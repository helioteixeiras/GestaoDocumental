namespace GestaoDocumental.Application.Models;

public sealed class FileDownloadContent : IDisposable
{
    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = "application/octet-stream";

    public long Size { get; init; }

    public Stream Content { get; init; } = Stream.Null;

    public void Dispose()
    {
        Content.Dispose();
    }
}
