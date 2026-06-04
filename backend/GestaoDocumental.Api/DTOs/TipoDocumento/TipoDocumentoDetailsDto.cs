namespace GestaoDocumental.Api.DTOs.TipoDocumento;

public class TipoDocumentoDetailsDto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int CategoriaDocumentoId { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAtualizacao { get; set; }
}
