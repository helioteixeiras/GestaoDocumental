using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Common;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class GenericService<T> : IGenericService<T> where T : BaseEntity
{
    protected readonly IGenericRepository<T> Repository;
    protected readonly IUnitOfWork UnitOfWork;

    public GenericService(
        IGenericRepository<T> repository,
        IUnitOfWork unitOfWork)
    {
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await Repository.AddAsync(entity);
        await UnitOfWork.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<bool> UpdateAsync(int id, T entity)
    {
        var existing = await Repository.GetByIdAsync(id);

        if (existing is null)
            return false;

        entity.Id = id;

        Repository.Update(entity);
        await UnitOfWork.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var existing = await Repository.GetByIdAsync(id);

        if (existing is null)
            return false;

        Repository.Delete(existing);
        await UnitOfWork.SaveChangesAsync();

        return true;
    }
}