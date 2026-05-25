namespace GestaoDocumental.Application.DTOs.Dashboard;

public class DashboardDocumentoRecenteDto
{
    public int DocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = string.Empty;

    public string? ReferenciaExterna { get; set; }

    public string? CodigoArquivo { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string EstadoAtual { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; }
}
