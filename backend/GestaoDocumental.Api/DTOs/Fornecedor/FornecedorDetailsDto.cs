namespace GestaoDocumental.Api.DTOs.Fornecedor;

public class FornecedorDetailsDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Nif { get; set; }

    public string? Endereco { get; set; }

    public string? ContactoPrincipal { get; set; }

    public string? ContactoAlternativo { get; set; }

    public string? Email1 { get; set; }

    public string? Email2 { get; set; }

    public string? PontoFocal { get; set; }

    public string? Notas { get; set; }
}
