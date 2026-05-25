using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class TramitacaoDocumento
{
    public int Id { get; set; }

    public int DocumentoId { get; set; }

    public int DirecaoOrigemId { get; set; }

    public int DirecaoDestinoId { get; set; }

    public int? ColaboradorOrigemId { get; set; }

    public int? ColaboradorDestinoId { get; set; }

    public string? Estado { get; set; }

    public string? Observacao { get; set; }

    public DateTime DataEnvio { get; set; }

    public DateTime? DataRececao { get; set; }

    public virtual Colaborador? ColaboradorDestino { get; set; }

    public virtual Colaborador? ColaboradorOrigem { get; set; }

    public virtual Direcao DirecaoDestino { get; set; } = null!;

    public virtual Direcao DirecaoOrigem { get; set; } = null!;

    public virtual Documento Documento { get; set; } = null!;
}
