using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IDocumentoAnexoService
{
    Task<IReadOnlyList<DocumentoAnexo>> GetAllAsync();
    Task<DocumentoAnexo?> GetByIdAsync(int id);
    Task<DocumentoAnexo> CreateAsync(DocumentoAnexo entity);
    Task<bool> UpdateAsync(int id, DocumentoAnexo entity);
    Task<bool> DeleteAsync(int id);
}
