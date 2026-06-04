using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class DocumentoComentario : BaseEntity
{
    public int DocumentoId { get; set; }

    public int UsuarioSistemaId { get; set; }

    public string Comentario { get; set; } = null!;

    public virtual Documento Documento { get; set; } = null!;

    public virtual UsuarioSistema UsuarioSistema { get; set; } = null!;
}
