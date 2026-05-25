using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class DocumentoAnexo : BaseEntity
{
    public int DocumentoId { get; set; }

    public Guid GuidFicheiro { get; set; }

    public string NomeOriginal { get; set; } = null!;

    public string NomeFisico { get; set; } = null!;

    public string? Extensao { get; set; }

    public string? Caminho { get; set; }

    public string? HashSha256 { get; set; }

    public long? Tamanho { get; set; }

    public DateTime DataUpload { get; set; }

    public virtual Documento Documento { get; set; } = null!;
}
