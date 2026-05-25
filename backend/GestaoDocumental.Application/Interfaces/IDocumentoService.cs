using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IDocumentoService
{
    Task<IReadOnlyList<Documento>> GetAllAsync();
    Task<Documento?> GetByIdAsync(int id);
    Task<Documento> CreateAsync(Documento entity);
    Task<bool> UpdateAsync(int id, Documento entity);
    Task<bool> DeleteAsync(int id);
}
