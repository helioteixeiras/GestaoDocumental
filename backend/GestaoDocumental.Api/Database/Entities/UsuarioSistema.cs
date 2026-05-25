using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class UsuarioSistema
{
    public int Id { get; set; }

    public int ColaboradorId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PasswordSalt { get; set; }

    public int EstadoLoginId { get; set; }

    public int? TentativasFalhadas { get; set; }

    public bool? Bloqueado { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public DateTime DataCriacao { get; set; }

    public bool? Ativo { get; set; }

    public virtual Colaborador Colaborador { get; set; } = null!;

    public virtual EstadoLogin EstadoLogin { get; set; } = null!;
}
