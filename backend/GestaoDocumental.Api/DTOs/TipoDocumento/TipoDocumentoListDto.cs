namespace GestaoDocumental.Api.DTOs.TipoDocumento;

public class TipoDocumentoListDto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int CategoriaDocumentoId { get; set; }

    public string CategoriaDocumentoNome { get; set; } = string.Empty;

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}
