using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class TipoDocumentoColaboradorService
    : GenericService<TipoDocumentoColaborador>,
      ITipoDocumentoColaboradorService
{
    public TipoDocumentoColaboradorService(
        IGenericRepository<TipoDocumentoColaborador> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
