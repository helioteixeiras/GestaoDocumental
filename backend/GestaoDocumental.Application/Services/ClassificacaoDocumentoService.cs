using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class ClassificacaoDocumentoService
    : GenericService<ClassificacaoDocumento>,
      IClassificacaoDocumentoService
{
    public ClassificacaoDocumentoService(
        IGenericRepository<ClassificacaoDocumento> repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }
}