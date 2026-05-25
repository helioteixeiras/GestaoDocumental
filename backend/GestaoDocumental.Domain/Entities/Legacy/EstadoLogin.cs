using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class EstadoLogin : BaseEntity
{
    public string Nome { get; set; } = null!;

    public virtual ICollection<UsuarioSistema> UsuarioSistemas { get; set; } = new List<UsuarioSistema>();
}
