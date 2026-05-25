using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class PostoTrabalhoService
    : GenericService<PostoTrabalho>,
      IPostoTrabalhoService
{
    public PostoTrabalhoService(
        IGenericRepository<PostoTrabalho> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}
