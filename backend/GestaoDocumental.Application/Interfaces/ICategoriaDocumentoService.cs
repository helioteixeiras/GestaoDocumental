using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface ICategoriaDocumentoService
{
    Task<IReadOnlyList<CategoriaDocumento>> GetAllAsync();
    Task<CategoriaDocumento?> GetByIdAsync(int id);
    Task<CategoriaDocumento> CreateAsync(CategoriaDocumento entity);
    Task<bool> UpdateAsync(int id, CategoriaDocumento entity);
    Task<bool> DeleteAsync(int id);
}
