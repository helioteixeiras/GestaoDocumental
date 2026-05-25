using GestaoDocumental.Application.Common;
using GestaoDocumental.Application.DTOs.Dashboard;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Domain.ReadModels;

namespace GestaoDocumental.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardDocumentosResumoDto> ObterResumoDocumentosAsync(
        CancellationToken cancellationToken = default)
    {
        var readModel = await _dashboardRepository.GetDocumentosResumoAsync(
            DashboardHistoricoConstants.DefaultRecentItemsLimit,
            cancellationToken);

        return new DashboardDocumentosResumoDto
        {
            TotalDocumentos = readModel.TotalDocumentos,
            TotalPendentes = readModel.TotalPendentes,
            TotalEmTramitacao = readModel.TotalEmTramitacao,
            TotalAprovados = readModel.TotalAprovados,
            TotalRejeitados = readModel.TotalRejeitados,
            TotalAnexosAtivos = readModel.TotalAnexosAtivos,
            TotalDownloads = readModel.TotalDownloads,
            DocumentosCriadosUltimos30Dias = readModel.DocumentosCriadosUltimos30Dias,
            ContadoresPorEstado = readModel.ContadoresPorEstado
                .Select(MapContadorPorEstado)
                .ToList(),
            UltimosDocumentosCriados = readModel.UltimosDocumentosCriados
                .Select(MapDocumentoRecente)
                .ToList(),
            UltimosDownloads = readModel.UltimosDownloads
                .Select(MapDownloadRecente)
                .ToList(),
            UltimosEventosWorkflow = readModel.UltimosEventosWorkflow
                .Select(MapWorkflowEvento)
                .ToList()
        };
    }

    private static DashboardContadorPorEstadoDto MapContadorPorEstado(
        DashboardContadorPorEstadoReadModel item) =>
        new()
        {
            Estado = item.Estado,
            Total = item.Total
        };

    private static DashboardDocumentoRecenteDto MapDocumentoRecente(
        DashboardDocumentoRecenteReadModel item) =>
        new()
        {
            DocumentoId = item.DocumentoId,
            NumeroDocumento = item.NumeroDocumento,
            ReferenciaExterna = item.ReferenciaExterna,
            CodigoArquivo = item.CodigoArquivo,
            Titulo = item.Titulo,
            EstadoAtual = item.EstadoAtual,
            DataCriacao = item.DataCriacao
        };

    private static DashboardDownloadRecenteDto MapDownloadRecente(
        DashboardDownloadRecenteReadModel item) =>
        new()
        {
            DocumentoId = item.DocumentoId,
            HistoricoId = item.HistoricoId,
            DataAcao = item.DataAcao,
            UsuarioNome = item.UsuarioNome,
            Acao = item.Acao,
            ObservacaoResumida = DashboardHistoricoConstants.ResumirObservacao(item.Observacao)
        };

    private static DashboardWorkflowEventoDto MapWorkflowEvento(
        DashboardWorkflowEventoReadModel item) =>
        new()
        {
            DocumentoId = item.DocumentoId,
            HistoricoId = item.HistoricoId,
            DataAcao = item.DataAcao,
            Acao = item.Acao,
            UsuarioNome = item.UsuarioNome,
            ObservacaoResumida = DashboardHistoricoConstants.ResumirObservacao(item.Observacao)
        };
}
