namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoDownloadReportItemDto
{
    public int HistoricoId { get; set; }

    public DateTime DataAcao { get; set; }

    public string Acao { get; set; } = string.Empty;

    public string? Observacao { get; set; }

    public int UsuarioId { get; set; }

    public string? UsuarioNome { get; set; }

    public int? AnexoId { get; set; }

    public int? Versao { get; set; }

    public string? HashSha256 { get; set; }
}
