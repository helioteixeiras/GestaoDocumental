using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class UsuarioSistema : BaseEntity
{
    public int? ColaboradorId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int PerfilId { get; set; }

    public int EstadoLoginId { get; set; }

    public int TentativasLogin { get; set; }

    public bool Bloqueado { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public virtual Colaborador? Colaborador { get; set; }

    public virtual Perfil Perfil { get; set; } = null!;

    public virtual EstadoLogin EstadoLogin { get; set; } = null!;
}
