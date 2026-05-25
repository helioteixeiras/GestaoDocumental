using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class Pais
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();

    public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
}
