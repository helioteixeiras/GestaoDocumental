namespace GestaoDocumental.Api.DTOs.Documento;

public class DocumentoCreateDto
{
    public string NumeroDocumento { get; set; } = string.Empty;

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public int TipoDocumentoId { get; set; }

    public int ClassificacaoId { get; set; }

    public int EstadoDocumentoId { get; set; }

    public int DirecaoOrigemId { get; set; }

    public int ColaboradorCriadorId { get; set; }

    public int? FornecedorId { get; set; }

    public DateTime? DataDocumento { get; set; }

    public DateTime? DataRecepcao { get; set; }

    public DateTime? PrazoResposta { get; set; }

    public string? ReferenciaExterna { get; set; }

    public string? PalavrasChave { get; set; }

    public string? Observacao { get; set; }

    public string? LocalizacaoFisica { get; set; }

    public string? CodigoArquivo { get; set; }
}
