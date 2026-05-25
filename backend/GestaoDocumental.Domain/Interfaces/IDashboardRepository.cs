using GestaoDocumental.Domain.ReadModels;

namespace GestaoDocumental.Domain.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardDocumentosReadModel> GetDocumentosResumoAsync(
        int recentItemsLimit = 10,
        CancellationToken cancellationToken = default);
}
