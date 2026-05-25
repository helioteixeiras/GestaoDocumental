namespace GestaoDocumental.Application.Common;

public static class FileSizeFormatter
{
    public static string Format(long bytes)
    {
        if (bytes < 1024)
            return $"{bytes} B";

        if (bytes < 1024 * 1024)
            return $"{bytes / 1024.0:0.##} KB";

        return $"{bytes / (1024.0 * 1024.0):0.##} MB";
    }
}
