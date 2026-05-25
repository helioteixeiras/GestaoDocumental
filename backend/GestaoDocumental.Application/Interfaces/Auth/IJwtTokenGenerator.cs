using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(UsuarioSistema usuario);

    DateTime GetTokenExpiration();
}
