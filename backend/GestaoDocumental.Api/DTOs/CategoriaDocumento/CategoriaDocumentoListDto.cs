namespace GestaoDocumental.Api.DTOs.CategoriaDocumento;

public class CategoriaDocumentoListDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Codigo { get; set; } = string.Empty;

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}
