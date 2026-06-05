using GestaoDocumental.Domain.Common;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly GestaoDocumentalDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(GestaoDocumentalDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void ApplyScalarValues(T existing, T source)
    {
        DetachDuplicateTrackedInstances(existing.Id, existing);

        var entry = _context.Entry(existing);

        foreach (var property in entry.Properties)
        {
            if (property.Metadata.IsPrimaryKey())
                continue;

            if (property.Metadata.Name is nameof(BaseEntity.DataCriacao))
                continue;

            if (property.Metadata.IsForeignKey())
                continue;

            var propertyInfo = property.Metadata.PropertyInfo;
            if (propertyInfo is null)
                continue;

            property.CurrentValue = propertyInfo.GetValue(source);
        }
    }

    private void DetachDuplicateTrackedInstances(int id, T keepTracked)
    {
        foreach (var trackedEntry in _context.ChangeTracker.Entries<T>().ToList())
        {
            if (trackedEntry.Entity.Id != id)
                continue;

            if (ReferenceEquals(trackedEntry.Entity, keepTracked))
                continue;

            trackedEntry.State = EntityState.Detached;
        }
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}