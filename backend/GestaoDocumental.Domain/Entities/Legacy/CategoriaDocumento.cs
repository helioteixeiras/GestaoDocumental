using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class CategoriaDocumento : BaseEntity
{
    public string Nome { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public virtual ICollection<TipoDocumento> TipoDocumentos { get; set; } = new List<TipoDocumento>();
}
