namespace GestaoDocumental.Application.Models;

public class StoredFileInfo
{
    public string RelativePath { get; set; } = string.Empty;

    public string PhysicalFileName { get; set; } = string.Empty;

    public string HashSha256 { get; set; } = string.Empty;

    public long Size { get; set; }
}
