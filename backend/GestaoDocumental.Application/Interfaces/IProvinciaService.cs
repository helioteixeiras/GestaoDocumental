using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IProvinciaService
{
    Task<IReadOnlyList<Provincia>> GetAllAsync();
    Task<Provincia?> GetByIdAsync(int id);
    Task<Provincia> CreateAsync(Provincia entity);
    Task<bool> UpdateAsync(int id, Provincia entity);
    Task<bool> DeleteAsync(int id);
}
