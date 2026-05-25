using System;
using System.Collections.Generic;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class TipoDocumentoColaborador
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();
}
