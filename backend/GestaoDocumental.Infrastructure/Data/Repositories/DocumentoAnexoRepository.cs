using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class DocumentoAnexoRepository
    : GenericRepository<DocumentoAnexo>,
      IDocumentoAnexoRepository
{
    public DocumentoAnexoRepository(GestaoDocumentalDbContext context)
        : base(context)
    {
    }

    public async Task<DocumentoAnexo?> GetLatestByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(anexo => anexo.DocumentoId == documentoId)
            .OrderByDescending(anexo => anexo.DataUpload)
            .ThenByDescending(anexo => anexo.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CountByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(anexo => anexo.DocumentoId == documentoId, cancellationToken);
    }
}
