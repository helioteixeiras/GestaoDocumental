using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IClassificacaoDocumentoService
{
    Task<IReadOnlyList<ClassificacaoDocumento>> GetAllAsync();
    Task<ClassificacaoDocumento?> GetByIdAsync(int id);
    Task<ClassificacaoDocumento> CreateAsync(ClassificacaoDocumento entity);
    Task<bool> UpdateAsync(int id, ClassificacaoDocumento entity);
    Task<bool> DeleteAsync(int id);
}