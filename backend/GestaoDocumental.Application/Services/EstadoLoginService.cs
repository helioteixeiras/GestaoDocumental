using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class EstadoLoginService
    : GenericService<EstadoLogin>,
      IEstadoLoginService
{
    public EstadoLoginService(
        IGenericRepository<EstadoLogin> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
