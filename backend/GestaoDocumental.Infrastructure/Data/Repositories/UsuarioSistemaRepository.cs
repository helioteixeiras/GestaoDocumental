using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class UsuarioSistemaRepository
    : GenericRepository<UsuarioSistema>,
      IUsuarioSistemaRepository
{
    public UsuarioSistemaRepository(GestaoDocumentalDbContext context)
        : base(context)
    {
    }

    public async Task<UsuarioSistema?> GetByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(usuario => usuario.Perfil)
            .FirstOrDefaultAsync(
                usuario => usuario.Username == username,
                cancellationToken);
    }

    public async Task<UsuarioSistema?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(
            usuario => usuario.Email == email,
            cancellationToken);
    }
}
