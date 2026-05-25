using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class ProvinciaService
    : GenericService<Provincia>,
      IProvinciaService
{
    public ProvinciaService(
        IGenericRepository<Provincia> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
