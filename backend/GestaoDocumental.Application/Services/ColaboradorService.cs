using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class ColaboradorService
    : GenericService<Colaborador>,
      IColaboradorService
{
    public ColaboradorService(
        IGenericRepository<Colaborador> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
