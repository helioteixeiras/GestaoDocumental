namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoAnexoListDto
{
    public int DocumentoId { get; set; }

    public int TotalAnexos { get; set; }

    public int? UltimaVersao { get; set; }

    public IReadOnlyList<DocumentoAnexoListItemDto> Anexos { get; set; } = [];
}
