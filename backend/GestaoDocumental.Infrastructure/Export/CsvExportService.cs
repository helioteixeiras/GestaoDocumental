using System.Text;
using GestaoDocumental.Application.Interfaces;

namespace GestaoDocumental.Infrastructure.Export;

public class CsvExportService : ICsvExportService
{
    public byte[] BuildCsv(
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<string?>> rows,
        char separator = ';')
    {
        var builder = new StringBuilder();
        builder.AppendLine(string.Join(separator, headers.Select(EscapeField)));

        foreach (var row in rows)
        {
            builder.AppendLine(string.Join(separator, row.Select(value => EscapeField(value ?? string.Empty))));
        }

        return Encoding.UTF8.GetPreamble()
            .Concat(Encoding.UTF8.GetBytes(builder.ToString()))
            .ToArray();
    }

    private static string EscapeField(string value)
    {
        if (value.Contains('"') ||
            value.Contains(';') ||
            value.Contains('\n') ||
            value.Contains('\r'))
        {
            return $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
        }

        return value;
    }
}
