using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Context;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GestaoDocumentalDbContext _context;

    public UnitOfWork(GestaoDocumentalDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}