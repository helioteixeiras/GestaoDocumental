using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities;

public class Utilizador : BaseEntity
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool PrimeiroAcesso { get; set; } = true;
}