using System;
using System.Collections.Generic;

namespace GestaoDocumental.Api.Database.Entities;

public partial class Documento
{
    public int Id { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public string? Descricao { get; set; }

    public int TipoDocumentoId { get; set; }

    public int ClassificacaoId { get; set; }

    public int EstadoDocumentoId { get; set; }

    public int DirecaoOrigemId { get; set; }

    public int ColaboradorCriadorId { get; set; }

    public int? FornecedorId { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataDocumento { get; set; }

    public DateTime? DataRecepcao { get; set; }

    public DateTime? PrazoResposta { get; set; }

    public string? ReferenciaExterna { get; set; }

    public string? PalavrasChave { get; set; }

    public string? Observacao { get; set; }

    public int? VersaoAtual { get; set; }

    public string? LocalizacaoFisica { get; set; }

    public string? CodigoArquivo { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public int? UtilizadorAtualizacaoId { get; set; }

    public virtual ClassificacaoDocumento Classificacao { get; set; } = null!;

    public virtual Colaborador ColaboradorCriador { get; set; } = null!;

    public virtual Direcao DirecaoOrigem { get; set; } = null!;

    public virtual ICollection<DocumentoAnexo> DocumentoAnexos { get; set; } = new List<DocumentoAnexo>();

    public virtual ICollection<DocumentoHistorico> DocumentoHistoricos { get; set; } = new List<DocumentoHistorico>();

    public virtual EstadoDocumento EstadoDocumento { get; set; } = null!;

    public virtual Fornecedor? Fornecedor { get; set; }

    public virtual TipoDocumento TipoDocumento { get; set; } = null!;

    public virtual ICollection<TramitacaoDocumento> TramitacaoDocumentos { get; set; } = new List<TramitacaoDocumento>();

    public virtual Colaborador? UtilizadorAtualizacao { get; set; }
}
