using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Pais : BaseEntity
{
    public string Nome { get; set; } = null!;

    public string? SiglaISO2 { get; set; }

    public string? CodigoIso3 { get; set; }

    public string? Capital { get; set; }

    public string? IndicativoTelefonico { get; set; }

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();

    public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
}
