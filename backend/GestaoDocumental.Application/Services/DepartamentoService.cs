using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class DepartamentoService
    : GenericService<Departamento>,
      IDepartamentoService
{
    public DepartamentoService(
        IGenericRepository<Departamento> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
