using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IEstadoColaboradorService
{
    Task<IReadOnlyList<EstadoColaborador>> GetAllAsync();
    Task<EstadoColaborador?> GetByIdAsync(int id);
    Task<EstadoColaborador> CreateAsync(EstadoColaborador entity);
    Task<bool> UpdateAsync(int id, EstadoColaborador entity);
    Task<bool> DeleteAsync(int id);
}
