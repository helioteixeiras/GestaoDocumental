using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IEstadoDocumentoService
{
    Task<IReadOnlyList<EstadoDocumento>> GetAllAsync();
    Task<EstadoDocumento?> GetByIdAsync(int id);
    Task<EstadoDocumento> CreateAsync(EstadoDocumento entity);
    Task<bool> UpdateAsync(int id, EstadoDocumento entity);
    Task<bool> DeleteAsync(int id);
}
