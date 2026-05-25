using GestaoDocumental.Application.Common;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Domain.ReadModels;
using GestaoDocumental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly GestaoDocumentalDbContext _context;

    public DashboardRepository(GestaoDocumentalDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDocumentosReadModel> GetDocumentosResumoAsync(
        int recentItemsLimit = 10,
        CancellationToken cancellationToken = default)
    {
        var trintaDiasAtras = DateTime.UtcNow.AddDays(-30);

        var documentosAtivos = _context.Documentos
            .AsNoTracking()
            .Where(documento => documento.Ativo);

        var totalDocumentos = await documentosAtivos.CountAsync(cancellationToken);

        var contadoresPorEstado = await documentosAtivos
            .GroupBy(documento => documento.EstadoDocumento.Nome)
            .Select(grupo => new DashboardContadorPorEstadoReadModel
            {
                Estado = grupo.Key,
                Total = grupo.Count()
            })
            .OrderByDescending(item => item.Total)
            .ThenBy(item => item.Estado)
            .ToListAsync(cancellationToken);

        var totalAnexosAtivos = await _context.DocumentoAnexos
            .AsNoTracking()
            .CountAsync(anexo => anexo.Ativo, cancellationToken);

        var totalDownloads = await _context.DocumentoHistoricos
            .AsNoTracking()
            .CountAsync(
                historico => DashboardHistoricoConstants.DownloadAcoes.Contains(historico.Acao),
                cancellationToken);

        var documentosCriadosUltimos30Dias = await documentosAtivos
            .CountAsync(documento => documento.DataCriacao >= trintaDiasAtras, cancellationToken);

        var ultimosDocumentosCriados = await documentosAtivos
            .OrderByDescending(documento => documento.DataCriacao)
            .ThenByDescending(documento => documento.Id)
            .Take(recentItemsLimit)
            .Select(documento => new DashboardDocumentoRecenteReadModel
            {
                DocumentoId = documento.Id,
                NumeroDocumento = documento.NumeroDocumento,
                ReferenciaExterna = documento.ReferenciaExterna,
                CodigoArquivo = documento.CodigoArquivo,
                Titulo = documento.Titulo,
                EstadoAtual = documento.EstadoDocumento.Nome,
                DataCriacao = documento.DataCriacao
            })
            .ToListAsync(cancellationToken);

        var ultimosDownloads = await _context.DocumentoHistoricos
            .AsNoTracking()
            .Where(historico => DashboardHistoricoConstants.DownloadAcoes.Contains(historico.Acao))
            .OrderByDescending(historico => historico.DataAcao)
            .ThenByDescending(historico => historico.Id)
            .Take(recentItemsLimit)
            .Select(historico => new DashboardDownloadRecenteReadModel
            {
                DocumentoId = historico.DocumentoId,
                HistoricoId = historico.Id,
                DataAcao = historico.DataAcao,
                UsuarioNome = historico.Utilizador.Nome,
                Acao = historico.Acao,
                Observacao = historico.Observacao
            })
            .ToListAsync(cancellationToken);

        var ultimosEventosWorkflow = await _context.DocumentoHistoricos
            .AsNoTracking()
            .Where(historico => DashboardHistoricoConstants.WorkflowAcoes.Contains(historico.Acao))
            .OrderByDescending(historico => historico.DataAcao)
            .ThenByDescending(historico => historico.Id)
            .Take(recentItemsLimit)
            .Select(historico => new DashboardWorkflowEventoReadModel
            {
                DocumentoId = historico.DocumentoId,
                HistoricoId = historico.Id,
                DataAcao = historico.DataAcao,
                Acao = historico.Acao,
                UsuarioNome = historico.Utilizador.Nome,
                Observacao = historico.Observacao
            })
            .ToListAsync(cancellationToken);

        return new DashboardDocumentosReadModel
        {
            TotalDocumentos = totalDocumentos,
            TotalPendentes = SomarPorEstado(contadoresPorEstado, DocumentoWorkflowConstants.EstadoPendente),
            TotalEmTramitacao = SomarPorEstado(contadoresPorEstado, DocumentoWorkflowConstants.EstadoEmTramitacao),
            TotalAprovados = SomarPorEstado(contadoresPorEstado, DocumentoWorkflowConstants.EstadoAprovado),
            TotalRejeitados = SomarPorEstado(contadoresPorEstado, DocumentoWorkflowConstants.EstadoRejeitado),
            TotalAnexosAtivos = totalAnexosAtivos,
            TotalDownloads = totalDownloads,
            DocumentosCriadosUltimos30Dias = documentosCriadosUltimos30Dias,
            ContadoresPorEstado = contadoresPorEstado,
            UltimosDocumentosCriados = ultimosDocumentosCriados,
            UltimosDownloads = ultimosDownloads,
            UltimosEventosWorkflow = ultimosEventosWorkflow
        };
    }

    private static int SomarPorEstado(
        IEnumerable<DashboardContadorPorEstadoReadModel> contadores,
        string estadoAlvo)
    {
        return contadores
            .Where(item => string.Equals(item.Estado, estadoAlvo, StringComparison.OrdinalIgnoreCase))
            .Sum(item => item.Total);
    }
}
