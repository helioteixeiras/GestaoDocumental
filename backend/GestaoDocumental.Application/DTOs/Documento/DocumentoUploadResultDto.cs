namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoUploadResultDto
{
    public int DocumentoId { get; set; }

    public int AnexoId { get; set; }

    public Guid GuidFicheiro { get; set; }

    public string NomeOriginal { get; set; } = string.Empty;

    public string Extensao { get; set; } = string.Empty;

    public long Tamanho { get; set; }

    public DateTime DataUpload { get; set; }

    public int Versao { get; set; }
}
