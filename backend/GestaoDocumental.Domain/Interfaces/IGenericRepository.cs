using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void ApplyScalarValues(T existing, T source);
    void Delete(T entity);
}