using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Provincia : BaseEntity
{
    public string Nome { get; set; } = null!;

    public int PaisId { get; set; }

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();

    public virtual Pais Pais { get; set; } = null!;
}
