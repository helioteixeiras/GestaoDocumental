using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class TipoDocumentoService
    : GenericService<TipoDocumento>,
      ITipoDocumentoService
{
    private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

    public TipoDocumentoService(
        ITipoDocumentoRepository repository,
        IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
        _tipoDocumentoRepository = repository;
    }

    public override async Task<IReadOnlyList<TipoDocumento>> GetAllAsync()
    {
        return await _tipoDocumentoRepository.GetAllWithCategoriaAsync();
    }
}
