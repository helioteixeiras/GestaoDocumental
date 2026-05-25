namespace GestaoDocumental.Application.Interfaces;

public interface ICsvExportService
{
    byte[] BuildCsv(
        IReadOnlyList<string> headers,
        IEnumerable<IReadOnlyList<string?>> rows,
        char separator = ';');
}
