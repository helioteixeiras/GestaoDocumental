using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface ITramitacaoDocumentoService
{
    Task<IReadOnlyList<TramitacaoDocumento>> GetAllAsync();
    Task<TramitacaoDocumento?> GetByIdAsync(int id);
    Task<TramitacaoDocumento> CreateAsync(TramitacaoDocumento entity);
    Task<bool> UpdateAsync(int id, TramitacaoDocumento entity);
    Task<bool> DeleteAsync(int id);
}
