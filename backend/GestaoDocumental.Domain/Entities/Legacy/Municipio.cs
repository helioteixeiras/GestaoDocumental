using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Municipio : BaseEntity
{
    public string Nome { get; set; } = null!;

    public int ProvinciaId { get; set; }

    public virtual ICollection<PostoTrabalho> PostoTrabalhos { get; set; } = new List<PostoTrabalho>();

    public virtual Provincia Provincia { get; set; } = null!;
}
