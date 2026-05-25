using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class UsuarioSistemaService
    : GenericService<UsuarioSistema>,
      IUsuarioSistemaService
{
    public UsuarioSistemaService(
        IGenericRepository<UsuarioSistema> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
