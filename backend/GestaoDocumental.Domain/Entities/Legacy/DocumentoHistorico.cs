using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class DocumentoHistorico : BaseEntity
{
    public int DocumentoId { get; set; }

    public int UtilizadorId { get; set; }

    public string Acao { get; set; } = null!;

    public string? Observacao { get; set; }

    public DateTime DataAcao { get; set; }

    public virtual Documento Documento { get; set; } = null!;

    public virtual Colaborador Utilizador { get; set; } = null!;
}
