namespace GestaoDocumental.Api.DTOs.Documento;

public class DocumentoListDto
{
    public int Id { get; set; }

    public string NumeroDocumento { get; set; } = string.Empty;

    public string Titulo { get; set; } = string.Empty;

    public int TipoDocumentoId { get; set; }

    public int ClassificacaoId { get; set; }

    public int EstadoDocumentoId { get; set; }

    public int DirecaoOrigemId { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataDocumento { get; set; }
}
