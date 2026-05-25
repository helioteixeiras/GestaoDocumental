using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.ReadModels;

namespace GestaoDocumental.Domain.Interfaces;

public interface IDocumentoWorkflowRepository
{
    Task<Documento?> GetDocumentoAsync(int documentoId, CancellationToken cancellationToken = default);

    Task<DocumentoWorkflowReadModel?> GetWorkflowByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default);

    Task<UsuarioSistema?> GetUsuarioAsync(int usuarioId, CancellationToken cancellationToken = default);

    Task<EstadoDocumento> EnsureEstadoDocumentoAsync(string nome, CancellationToken cancellationToken = default);

    Task<TramitacaoDocumento?> GetUltimaTramitacaoAsync(int documentoId, CancellationToken cancellationToken = default);

    Task AddTramitacaoAsync(TramitacaoDocumento tramitacao, CancellationToken cancellationToken = default);

    Task AddHistoricoAsync(DocumentoHistorico historico, CancellationToken cancellationToken = default);

    Task UpdateDocumentoAsync(Documento documento, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DocumentoHistorico>> GetDownloadHistoricoByDocumentoIdAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        int? usuarioId,
        string? acao,
        CancellationToken cancellationToken = default);

    Task<int> CountDownloadHistoricoByDocumentoIdAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        int? usuarioId,
        string? acao,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DocumentoHistorico>> GetDownloadHistoricoByDocumentoIdPagedAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        int? usuarioId,
        string? acao,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}
