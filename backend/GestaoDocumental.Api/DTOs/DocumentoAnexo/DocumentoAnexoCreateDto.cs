namespace GestaoDocumental.Api.DTOs.DocumentoAnexo;

public class DocumentoAnexoCreateDto
{
    public int DocumentoId { get; set; }

    public string NomeOriginal { get; set; } = string.Empty;

    public string? Extensao { get; set; }

    public long? Tamanho { get; set; }
}
