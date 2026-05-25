namespace GestaoDocumental.Shared.Settings;

public class StorageSettings
{
    public const string SectionName = "Storage";

    public string BasePath { get; set; } = "storage/documentos";

    public int MaxFileSizeMb { get; set; } = 10;

    public string[] AllowedExtensions { get; set; } =
    [
        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".png", ".jpg", ".jpeg"
    ];
}
