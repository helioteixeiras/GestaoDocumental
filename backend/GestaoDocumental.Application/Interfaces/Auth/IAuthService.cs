using GestaoDocumental.Application.DTOs.Auth;
using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);

    Task<UsuarioSistema> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default);

    Task<bool> ValidatePasswordAsync(string username, string password, CancellationToken cancellationToken = default);

    string HashPassword(string password);
}
