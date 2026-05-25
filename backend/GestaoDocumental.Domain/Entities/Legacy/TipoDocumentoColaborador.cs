using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class TipoDocumentoColaborador : BaseEntity
{
    public string Nome { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();
}
