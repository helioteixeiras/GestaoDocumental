namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoWorkflowResumoDto
{
    public int DocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = string.Empty;

    public string Titulo { get; set; } = string.Empty;

    public string EstadoAtual { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; }

    public int TotalTramitacoes { get; set; }

    public int TotalHistorico { get; set; }

    public string? UltimaAcao { get; set; }
}
