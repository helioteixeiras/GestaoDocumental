using GestaoDocumental.Domain.Common;
using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Domain.Interfaces;

public interface IUsuarioSistemaRepository : IGenericRepository<UsuarioSistema>
{
    Task<UsuarioSistema?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    Task<UsuarioSistema?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
