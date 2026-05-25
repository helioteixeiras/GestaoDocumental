using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class Provincia
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int PaisId { get; set; }

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();

    public virtual Pais Pais { get; set; } = null!;
}
