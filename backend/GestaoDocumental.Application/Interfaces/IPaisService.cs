using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IPaisService
{
    Task<IReadOnlyList<Pais>> GetAllAsync();
    Task<Pais?> GetByIdAsync(int id);
    Task<Pais> CreateAsync(Pais entity);
    Task<bool> UpdateAsync(int id, Pais entity);
    Task<bool> DeleteAsync(int id);
}
