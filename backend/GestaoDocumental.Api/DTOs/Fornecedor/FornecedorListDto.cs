namespace GestaoDocumental.Api.DTOs.Fornecedor;

public class FornecedorListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Nif { get; set; }

    public string? ContactoPrincipal { get; set; }

    public string? Email1 { get; set; }
}
