using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
