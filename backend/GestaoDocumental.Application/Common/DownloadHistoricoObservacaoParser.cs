using System.Text.RegularExpressions;

namespace GestaoDocumental.Application.Common;

public static partial class DownloadHistoricoObservacaoParser
{
    [GeneratedRegex(@"AnexoId=(\d+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex AnexoIdRegex();

    [GeneratedRegex(@"Versao=(\d+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex VersaoRegex();

    [GeneratedRegex(@"HashSha256=([^|]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex HashRegex();

    public static int? ExtractAnexoId(string? observacao)
    {
        if (string.IsNullOrWhiteSpace(observacao))
            return null;

        var match = AnexoIdRegex().Match(observacao);
        return match.Success && int.TryParse(match.Groups[1].Value, out var anexoId)
            ? anexoId
            : null;
    }

    public static int? ExtractVersao(string? observacao)
    {
        if (string.IsNullOrWhiteSpace(observacao))
            return null;

        var match = VersaoRegex().Match(observacao);
        return match.Success && int.TryParse(match.Groups[1].Value, out var versao)
            ? versao
            : null;
    }

    public static string? ExtractHashSha256(string? observacao)
    {
        if (string.IsNullOrWhiteSpace(observacao))
            return null;

        var match = HashRegex().Match(observacao);
        return match.Success ? match.Groups[1].Value.Trim() : null;
    }
}
