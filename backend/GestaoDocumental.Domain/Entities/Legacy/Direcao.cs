using System;
using System.Collections.Generic;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Direcao
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Sigla { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual ICollection<TramitacaoDocumento> TramitacaoDocumentoDirecaoDestinos { get; set; } = new List<TramitacaoDocumento>();

    public virtual ICollection<TramitacaoDocumento> TramitacaoDocumentoDirecaoOrigems { get; set; } = new List<TramitacaoDocumento>();
}
