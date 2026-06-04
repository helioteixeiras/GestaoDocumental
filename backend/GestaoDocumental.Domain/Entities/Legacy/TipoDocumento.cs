using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class TipoDocumento : BaseEntity
{
    public string Codigo { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public int CategoriaDocumentoId { get; set; }

    public virtual CategoriaDocumento CategoriaDocumento { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
