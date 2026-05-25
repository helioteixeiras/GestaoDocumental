using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Domain.Interfaces;

public interface IDocumentoAnexoRepository : IGenericRepository<DocumentoAnexo>
{
    Task<DocumentoAnexo?> GetLatestByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default);

    Task<int> CountByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default);
}
