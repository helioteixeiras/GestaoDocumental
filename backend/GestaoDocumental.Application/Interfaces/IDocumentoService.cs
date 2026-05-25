using GestaoDocumental.Application.DTOs.Documento;
using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IDocumentoService
{
    Task<IReadOnlyList<Documento>> GetAllAsync();
    Task<Documento?> GetByIdAsync(int id);
    Task<Documento> CreateAsync(Documento entity);
    Task<bool> UpdateAsync(int id, Documento entity);
    Task<bool> DeleteAsync(int id);

    Task<DocumentoWorkflowTimelineDto> ObterWorkflowDocumentoAsync(
        int documentoId,
        CancellationToken cancellationToken = default);

    Task<DocumentoUploadResultDto> UploadArquivoAsync(
        int documentoId,
        int usuarioSistemaId,
        string fileName,
        long fileLength,
        Stream content,
        CancellationToken cancellationToken = default);

    Task<DocumentoDownloadResultDto?> DownloadArquivoAsync(
        int documentoId,
        int usuarioSistemaId,
        CancellationToken cancellationToken = default);

    Task<DocumentoAnexoListDto> ListarAnexosAsync(
        int documentoId,
        CancellationToken cancellationToken = default);

    Task<DocumentoDownloadResultDto?> DownloadAnexoAsync(
        int documentoId,
        int anexoId,
        int usuarioSistemaId,
        CancellationToken cancellationToken = default);

    Task<DocumentoDownloadReportDto> ObterRelatorioDownloadsAsync(
        int documentoId,
        DateTime? dataInicio,
        DateTime? dataFim,
        int? usuarioId,
        string? acao,
        CancellationToken cancellationToken = default);
}
