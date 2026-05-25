using GestaoDocumental.Domain.Entities.Legacy;

namespace GestaoDocumental.Domain.ReadModels;

public class DocumentoWorkflowReadModel
{
    public Documento Documento { get; init; } = null!;

    public IReadOnlyList<DocumentoHistorico> Historicos { get; init; } = [];

    public IReadOnlyList<TramitacaoDocumento> Tramitacoes { get; init; } = [];
}
