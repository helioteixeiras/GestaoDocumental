using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Genero : BaseEntity
{
    public string Nome { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();
}
