using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class DocumentoHistoricoService
    : GenericService<DocumentoHistorico>,
      IDocumentoHistoricoService
{
    public DocumentoHistoricoService(
        IGenericRepository<DocumentoHistorico> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
