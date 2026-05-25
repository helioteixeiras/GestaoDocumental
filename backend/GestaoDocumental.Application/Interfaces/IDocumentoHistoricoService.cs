using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces;

public interface IDocumentoHistoricoService
{
    Task<IReadOnlyList<DocumentoHistorico>> GetAllAsync();
    Task<DocumentoHistorico?> GetByIdAsync(int id);
    Task<DocumentoHistorico> CreateAsync(DocumentoHistorico entity);
    Task<bool> UpdateAsync(int id, DocumentoHistorico entity);
    Task<bool> DeleteAsync(int id);
}
