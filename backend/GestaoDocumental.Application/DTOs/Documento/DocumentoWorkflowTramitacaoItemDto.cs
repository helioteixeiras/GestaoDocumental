namespace GestaoDocumental.Application.DTOs.Documento;

public class DocumentoWorkflowTramitacaoItemDto
{
    public int Id { get; set; }

    public DateTime DataEnvio { get; set; }

    public DateTime? DataRececao { get; set; }

    public string? Estado { get; set; }

    public string? Observacao { get; set; }

    public string? ColaboradorOrigem { get; set; }

    public string? ColaboradorDestino { get; set; }

    public string? DirecaoOrigem { get; set; }

    public string? DirecaoDestino { get; set; }
}
