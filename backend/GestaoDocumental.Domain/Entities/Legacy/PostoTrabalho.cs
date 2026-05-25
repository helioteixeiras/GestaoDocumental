using System;
using System.Collections.Generic;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class PostoTrabalho
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Sigla { get; set; }

    public int DepartamentoId { get; set; }

    public int MunicipioId { get; set; }

    public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual Municipio Municipio { get; set; } = null!;
}
