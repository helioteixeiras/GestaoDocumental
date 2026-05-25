using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IDirecaoService
{
    Task<IReadOnlyList<Direcao>> GetAllAsync();
    Task<Direcao?> GetByIdAsync(int id);
    Task<Direcao> CreateAsync(Direcao entity);
    Task<bool> UpdateAsync(int id, Direcao entity);
    Task<bool> DeleteAsync(int id);
}
