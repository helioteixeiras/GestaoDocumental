using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class EstadoColaboradorService
    : GenericService<EstadoColaborador>,
      IEstadoColaboradorService
{
    public EstadoColaboradorService(
        IGenericRepository<EstadoColaborador> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
