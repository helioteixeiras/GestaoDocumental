using GestaoDocumental.Application.DTOs.TramitacaoDocumento;
using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface ITramitacaoDocumentoService
{
    Task<IReadOnlyList<TramitacaoDocumento>> GetAllAsync();
    Task<TramitacaoDocumento?> GetByIdAsync(int id);
    Task<TramitacaoDocumento> CreateAsync(TramitacaoDocumento entity);
    Task<bool> UpdateAsync(int id, TramitacaoDocumento entity);
    Task<bool> DeleteAsync(int id);

    Task<DocumentoWorkflowResultDto> AprovarDocumentoAsync(
        int documentoId,
        int usuarioSistemaId,
        AprovarDocumentoDto request,
        CancellationToken cancellationToken = default);

    Task<DocumentoWorkflowResultDto> RejeitarDocumentoAsync(
        int documentoId,
        int usuarioSistemaId,
        RejeitarDocumentoDto request,
        CancellationToken cancellationToken = default);

    Task<DocumentoWorkflowResultDto> EncaminharDocumentoAsync(
        int documentoId,
        int usuarioSistemaId,
        EncaminharDocumentoDto request,
        CancellationToken cancellationToken = default);
}
