using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> ObterPorIdAsync(int id);

    Task<IEnumerable<T>> ObterTodosAsync();

    Task AdicionarAsync(T entity);

    void Atualizar(T entity);

    void Remover(T entity);
}