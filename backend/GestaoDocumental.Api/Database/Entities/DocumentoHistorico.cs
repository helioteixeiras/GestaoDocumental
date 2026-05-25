using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class DocumentoHistorico
{
    public int Id { get; set; }

    public int DocumentoId { get; set; }

    public int UtilizadorId { get; set; }

    public string Acao { get; set; } = null!;

    public string? Observacao { get; set; }

    public DateTime DataAcao { get; set; }

    public virtual Documento Documento { get; set; } = null!;

    public virtual Colaborador Utilizador { get; set; } = null!;
}
