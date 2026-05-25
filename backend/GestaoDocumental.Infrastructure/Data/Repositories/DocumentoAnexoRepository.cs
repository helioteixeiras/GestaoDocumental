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
        return await ActiveOnly(_dbSet)
            .Where(anexo => anexo.DocumentoId == documentoId)
            .OrderByDescending(anexo => anexo.DataUpload)
            .ThenByDescending(anexo => anexo.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CountByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return await ActiveOnly(_dbSet)
            .CountAsync(anexo => anexo.DocumentoId == documentoId, cancellationToken);
    }

    public Task<IReadOnlyList<DocumentoAnexo>> GetByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return GetActiveByDocumentoIdAsync(documentoId, cancellationToken);
    }

    public Task<DocumentoAnexo?> GetByDocumentoIdAndAnexoIdAsync(
        int documentoId,
        int anexoId,
        CancellationToken cancellationToken = default)
    {
        return GetActiveByDocumentoIdAndAnexoIdAsync(documentoId, anexoId, cancellationToken);
    }

    public async Task<IReadOnlyList<DocumentoAnexo>> GetActiveByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return await ActiveOnly(_dbSet)
            .Where(anexo => anexo.DocumentoId == documentoId)
            .OrderByDescending(anexo => anexo.DataUpload)
            .ThenByDescending(anexo => anexo.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<DocumentoAnexo?> GetActiveByDocumentoIdAndAnexoIdAsync(
        int documentoId,
        int anexoId,
        CancellationToken cancellationToken = default)
    {
        return await ActiveOnly(_dbSet)
            .FirstOrDefaultAsync(
                anexo => anexo.DocumentoId == documentoId && anexo.Id == anexoId,
                cancellationToken);
    }

    public Task SoftDeleteAsync(
        DocumentoAnexo anexo,
        CancellationToken cancellationToken = default)
    {
        anexo.Ativo = false;
        anexo.DataAtualizacao = DateTime.UtcNow;
        Update(anexo);
        return Task.CompletedTask;
    }

    private static IQueryable<DocumentoAnexo> ActiveOnly(IQueryable<DocumentoAnexo> query) =>
        query.Where(anexo => anexo.Ativo);
}
