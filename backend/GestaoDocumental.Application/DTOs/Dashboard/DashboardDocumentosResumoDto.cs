namespace GestaoDocumental.Application.DTOs.Dashboard;

public class DashboardDocumentosResumoDto
{
    public int TotalDocumentos { get; set; }

    public int TotalPendentes { get; set; }

    public int TotalEmTramitacao { get; set; }

    public int TotalAprovados { get; set; }

    public int TotalRejeitados { get; set; }

    public int TotalAnexosAtivos { get; set; }

    public int TotalDownloads { get; set; }

    public int DocumentosCriadosUltimos30Dias { get; set; }

    public IReadOnlyList<DashboardContadorPorEstadoDto> ContadoresPorEstado { get; set; } = [];

    public IReadOnlyList<DashboardDocumentoRecenteDto> UltimosDocumentosCriados { get; set; } = [];

    public IReadOnlyList<DashboardDownloadRecenteDto> UltimosDownloads { get; set; } = [];

    public IReadOnlyList<DashboardWorkflowEventoDto> UltimosEventosWorkflow { get; set; } = [];
}
