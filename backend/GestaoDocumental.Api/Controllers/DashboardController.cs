using GestaoDocumental.Application.DTOs.Dashboard;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet("documentos/resumo")]
    public async Task<ActionResult<DashboardDocumentosResumoDto>> GetDocumentosResumo(
        CancellationToken cancellationToken)
    {
        var result = await _dashboardService.ObterResumoDocumentosAsync(cancellationToken);
        return Ok(result);
    }
}
