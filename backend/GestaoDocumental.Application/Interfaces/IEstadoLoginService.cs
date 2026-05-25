using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IEstadoLoginService
{
    Task<IReadOnlyList<EstadoLogin>> GetAllAsync();
    Task<EstadoLogin?> GetByIdAsync(int id);
    Task<EstadoLogin> CreateAsync(EstadoLogin entity);
    Task<bool> UpdateAsync(int id, EstadoLogin entity);
    Task<bool> DeleteAsync(int id);
}
