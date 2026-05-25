using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class Departamento
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Sigla { get; set; }

    public int DirecaoId { get; set; }

    public virtual Direcao Direcao { get; set; } = null!;

    public virtual ICollection<PostoTrabalho> PostoTrabalhos { get; set; } = new List<PostoTrabalho>();
}
