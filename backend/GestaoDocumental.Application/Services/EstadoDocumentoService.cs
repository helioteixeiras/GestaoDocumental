using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class EstadoDocumentoService
    : GenericService<EstadoDocumento>,
      IEstadoDocumentoService
{
    public EstadoDocumentoService(
        IGenericRepository<EstadoDocumento> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
