using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class GeneroService
    : GenericService<Genero>,
      IGeneroService
{
    public GeneroService(
        IGenericRepository<Genero> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
