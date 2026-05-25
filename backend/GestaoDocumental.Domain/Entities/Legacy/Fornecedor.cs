using System;
using System.Collections.Generic;

namespace GestaoDocumental.Domain.Entities.Legacy;

public partial class Fornecedor
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Nif { get; set; }

    public string? Endereco { get; set; }

    public string? ContactoPrincipal { get; set; }

    public string? ContactoAlternativo { get; set; }

    public string? Email1 { get; set; }

    public string? Email2 { get; set; }

    public string? PontoFocal { get; set; }

    public string? Notas { get; set; }

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
