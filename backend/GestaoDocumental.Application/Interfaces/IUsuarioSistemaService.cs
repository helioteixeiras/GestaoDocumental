using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IUsuarioSistemaService
{
    Task<IReadOnlyList<UsuarioSistema>> GetAllAsync();
    Task<UsuarioSistema?> GetByIdAsync(int id);
    Task<UsuarioSistema> CreateAsync(UsuarioSistema entity);
    Task<bool> UpdateAsync(int id, UsuarioSistema entity);
    Task<bool> DeleteAsync(int id);
}
