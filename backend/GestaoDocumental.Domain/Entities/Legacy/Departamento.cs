using GestaoDocumental.Domain.Common;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Departamento : BaseEntity
{
    public string Nome { get; set; } = null!;

    public string? Sigla { get; set; }

    public int DirecaoId { get; set; }

    public virtual Direcao Direcao { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();

    public virtual ICollection<PostoTrabalho> PostoTrabalhos { get; set; } = new List<PostoTrabalho>();
}
