using FluentValidation;
using FluentValidation.Results;
using GestaoDocumental.Application.DTOs.Auth;
using GestaoDocumental.Application.Interfaces.Auth;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUsuarioSistemaRepository _usuarioSistemaRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUsuarioSistemaRepository usuarioSistemaRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _usuarioSistemaRepository = usuarioSistemaRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioSistemaRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (usuario is null || usuario.Bloqueado || !usuario.Ativo)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        if (!_passwordHasher.VerifyPassword(request.Password, usuario.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        usuario.UltimoLogin = DateTime.UtcNow;
        _usuarioSistemaRepository.Update(usuario);
        await _unitOfWork.SaveChangesAsync();

        var expiresAt = _jwtTokenGenerator.GetTokenExpiration();

        return new LoginResponseDto
        {
            Token = _jwtTokenGenerator.GenerateToken(usuario),
            ExpiresAt = expiresAt,
            Username = usuario.Username,
            Email = usuario.Email,
            Role = usuario.Perfil.Nome
        };
    }

    public async Task<UsuarioSistema> RegisterAsync(
        RegisterRequestDto request,
        CancellationToken cancellationToken = default)
    {
        if (await _usuarioSistemaRepository.GetByUsernameAsync(request.Username, cancellationToken) is not null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(nameof(request.Username), $"Username '{request.Username}' is already registered.")
            });
        }

        if (await _usuarioSistemaRepository.GetByEmailAsync(request.Email, cancellationToken) is not null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(nameof(request.Email), $"Email '{request.Email}' is already registered.")
            });
        }

        var usuario = new UsuarioSistema
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            PerfilId = request.PerfilId,
            EstadoLoginId = request.EstadoLoginId,
            ColaboradorId = request.ColaboradorId,
            TentativasLogin = 0,
            Bloqueado = false,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        await _usuarioSistemaRepository.AddAsync(usuario);
        await _unitOfWork.SaveChangesAsync();

        return usuario;
    }

    public async Task<bool> ValidatePasswordAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioSistemaRepository.GetByUsernameAsync(username, cancellationToken);

        if (usuario is null || usuario.Bloqueado || !usuario.Ativo)
            return false;

        return _passwordHasher.VerifyPassword(password, usuario.PasswordHash);
    }

    public string HashPassword(string password) =>
        _passwordHasher.HashPassword(password);
}
