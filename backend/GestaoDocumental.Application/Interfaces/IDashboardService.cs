using GestaoDocumental.Application.DTOs.Dashboard;

namespace GestaoDocumental.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDocumentosResumoDto> ObterResumoDocumentosAsync(
        CancellationToken cancellationToken = default);
}
