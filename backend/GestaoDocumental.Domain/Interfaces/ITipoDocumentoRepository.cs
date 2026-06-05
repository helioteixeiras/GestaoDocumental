using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Domain.Interfaces;

public interface ITipoDocumentoRepository : IGenericRepository<TipoDocumento>
{
    Task<IReadOnlyList<TipoDocumento>> GetAllWithCategoriaAsync();
}
