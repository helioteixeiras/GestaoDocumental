using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class DocumentoAnexoService
    : GenericService<DocumentoAnexo>,
      IDocumentoAnexoService
{
    public DocumentoAnexoService(
        IGenericRepository<DocumentoAnexo> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
