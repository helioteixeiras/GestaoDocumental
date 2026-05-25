using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IDepartamentoService
{
    Task<IReadOnlyList<Departamento>> GetAllAsync();
    Task<Departamento?> GetByIdAsync(int id);
    Task<Departamento> CreateAsync(Departamento entity);
    Task<bool> UpdateAsync(int id, Departamento entity);
    Task<bool> DeleteAsync(int id);
}
