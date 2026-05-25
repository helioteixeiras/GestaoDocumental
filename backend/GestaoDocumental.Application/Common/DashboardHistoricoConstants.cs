namespace GestaoDocumental.Application.Common;

public static class DashboardHistoricoConstants
{
    public static readonly string[] DownloadAcoes =
    [
        "DownloadArquivo",
        "DownloadArquivoVersao"
    ];

    public static readonly string[] WorkflowAcoes =
    [
        DocumentoWorkflowConstants.AcaoAprovacao,
        DocumentoWorkflowConstants.AcaoRejeicao,
        DocumentoWorkflowConstants.AcaoEncaminhamento
    ];

    public const int DefaultRecentItemsLimit = 10;
    public const int ObservacaoResumidaMaxLength = 150;

    public static string ResumirObservacao(string? observacao)
    {
        if (string.IsNullOrWhiteSpace(observacao))
            return string.Empty;

        var texto = observacao.Trim();

        if (texto.Length <= ObservacaoResumidaMaxLength)
            return texto;

        return $"{texto[..ObservacaoResumidaMaxLength]}...";
    }
}
