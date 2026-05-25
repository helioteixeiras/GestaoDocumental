using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class Municipio
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int ProvinciaId { get; set; }

    public virtual ICollection<PostoTrabalho> PostoTrabalhos { get; set; } = new List<PostoTrabalho>();

    public virtual Provincia Provincia { get; set; } = null!;
}
