using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IColaboradorService
{
    Task<IReadOnlyList<Colaborador>> GetAllAsync();
    Task<Colaborador?> GetByIdAsync(int id);
    Task<Colaborador> CreateAsync(Colaborador entity);
    Task<bool> UpdateAsync(int id, Colaborador entity);
    Task<bool> DeleteAsync(int id);
}
