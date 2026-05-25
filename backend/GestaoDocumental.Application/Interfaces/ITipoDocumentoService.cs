using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface ITipoDocumentoService
{
    Task<IReadOnlyList<TipoDocumento>> GetAllAsync();
    Task<TipoDocumento?> GetByIdAsync(int id);
    Task<TipoDocumento> CreateAsync(TipoDocumento entity);
    Task<bool> UpdateAsync(int id, TipoDocumento entity);
    Task<bool> DeleteAsync(int id);
}
