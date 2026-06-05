using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class TipoDocumentoRepository
    : GenericRepository<TipoDocumento>,
      ITipoDocumentoRepository
{
    public TipoDocumentoRepository(GestaoDocumentalDbContext context)
        : base(context)
    {
    }

    public async Task<IReadOnlyList<TipoDocumento>> GetAllWithCategoriaAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .Include(tipo => tipo.CategoriaDocumento)
            .OrderBy(tipo => tipo.Nome)
            .ToListAsync();
    }
}
