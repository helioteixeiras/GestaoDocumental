using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Perfil : BaseEntity
{
    public string Nome { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();

    public virtual ICollection<UsuarioSistema> UsuarioSistemas { get; set; } = new List<UsuarioSistema>();
}
