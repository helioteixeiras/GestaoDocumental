namespace GestaoDocumental.Api.DTOs.DocumentoAnexo;

public class DocumentoAnexoListDto
{
    public int Id { get; set; }

    public int DocumentoId { get; set; }

    public string NomeOriginal { get; set; } = string.Empty;

    public string? Extensao { get; set; }

    public long? Tamanho { get; set; }

    public DateTime DataUpload { get; set; }
}
