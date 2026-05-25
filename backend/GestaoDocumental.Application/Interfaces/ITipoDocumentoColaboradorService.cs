using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface ITipoDocumentoColaboradorService
{
    Task<IReadOnlyList<TipoDocumentoColaborador>> GetAllAsync();
    Task<TipoDocumentoColaborador?> GetByIdAsync(int id);
    Task<TipoDocumentoColaborador> CreateAsync(TipoDocumentoColaborador entity);
    Task<bool> UpdateAsync(int id, TipoDocumentoColaborador entity);
    Task<bool> DeleteAsync(int id);
}
