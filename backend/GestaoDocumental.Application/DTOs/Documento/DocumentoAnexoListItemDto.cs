namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoAnexoListItemDto
{
    public int Id { get; set; }

    public int DocumentoId { get; set; }

    public string NomeOriginal { get; set; } = string.Empty;

    public string? Extensao { get; set; }

    public long Tamanho { get; set; }

    public string TamanhoFormatado { get; set; } = string.Empty;

    public string? HashSha256 { get; set; }

    public DateTime DataUpload { get; set; }

    public int Versao { get; set; }

    public bool EhUltimaVersao { get; set; }

    public string DownloadUrl { get; set; } = string.Empty;
}
