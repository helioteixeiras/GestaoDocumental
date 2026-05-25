using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IPerfilService
{
    Task<IReadOnlyList<Perfil>> GetAllAsync();
    Task<Perfil?> GetByIdAsync(int id);
    Task<Perfil> CreateAsync(Perfil entity);
    Task<bool> UpdateAsync(int id, Perfil entity);
    Task<bool> DeleteAsync(int id);
}
