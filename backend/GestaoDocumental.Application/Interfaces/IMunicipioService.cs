using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IMunicipioService
{
    Task<IReadOnlyList<Municipio>> GetAllAsync();
    Task<Municipio?> GetByIdAsync(int id);
    Task<Municipio> CreateAsync(Municipio entity);
    Task<bool> UpdateAsync(int id, Municipio entity);
    Task<bool> DeleteAsync(int id);
}
