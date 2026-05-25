using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IFornecedorService
{
    Task<IReadOnlyList<Fornecedor>> GetAllAsync();
    Task<Fornecedor?> GetByIdAsync(int id);
    Task<Fornecedor> CreateAsync(Fornecedor entity);
    Task<bool> UpdateAsync(int id, Fornecedor entity);
    Task<bool> DeleteAsync(int id);
}
