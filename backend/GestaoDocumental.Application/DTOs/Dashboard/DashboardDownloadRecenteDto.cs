namespace GestaoDocumental.Application.DTOs.Dashboard;

public class DashboardDownloadRecenteDto
{
    public int DocumentoId { get; set; }

    public int HistoricoId { get; set; }

    public DateTime DataAcao { get; set; }

    public string? UsuarioNome { get; set; }

    public string Acao { get; set; } = string.Empty;

    public string? ObservacaoResumida { get; set; }
}
