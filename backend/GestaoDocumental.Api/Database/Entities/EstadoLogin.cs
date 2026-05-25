using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class EstadoLogin
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<UsuarioSistema> UsuarioSistemas { get; set; } = new List<UsuarioSistema>();
}
