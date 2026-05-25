namespace GestaoDocumental.Domain.ReadModels;

public class DashboardDocumentosReadModel
{
    public int TotalDocumentos { get; init; }

    public int TotalPendentes { get; init; }

    public int TotalEmTramitacao { get; init; }

    public int TotalAprovados { get; init; }

    public int TotalRejeitados { get; init; }

    public int TotalAnexosAtivos { get; init; }

    public int TotalDownloads { get; init; }

    public int DocumentosCriadosUltimos30Dias { get; init; }

    public IReadOnlyList<DashboardContadorPorEstadoReadModel> ContadoresPorEstado { get; init; } = [];

    public IReadOnlyList<DashboardDocumentoRecenteReadModel> UltimosDocumentosCriados { get; init; } = [];

    public IReadOnlyList<DashboardDownloadRecenteReadModel> UltimosDownloads { get; init; } = [];

    public IReadOnlyList<DashboardWorkflowEventoReadModel> UltimosEventosWorkflow { get; init; } = [];
}

public class DashboardContadorPorEstadoReadModel
{
    public string Estado { get; init; } = string.Empty;

    public int Total { get; init; }
}

public class DashboardDocumentoRecenteReadModel
{
    public int DocumentoId { get; init; }

    public string NumeroDocumento { get; init; } = string.Empty;

    public string? ReferenciaExterna { get; init; }

    public string? CodigoArquivo { get; init; }

    public string Titulo { get; init; } = string.Empty;

    public string EstadoAtual { get; init; } = string.Empty;

    public DateTime DataCriacao { get; init; }
}

public class DashboardDownloadRecenteReadModel
{
    public int DocumentoId { get; init; }

    public int HistoricoId { get; init; }

    public DateTime DataAcao { get; init; }

    public string? UsuarioNome { get; init; }

    public string Acao { get; init; } = string.Empty;

    public string? Observacao { get; init; }
}

public class DashboardWorkflowEventoReadModel
{
    public int DocumentoId { get; init; }

    public int HistoricoId { get; init; }

    public DateTime DataAcao { get; init; }

    public string Acao { get; init; } = string.Empty;

    public string? UsuarioNome { get; init; }

    public string? Observacao { get; init; }
}
