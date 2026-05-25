using System.Security.Claims;
using GestaoDocumental.Application.DTOs.TramitacaoDocumento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TramitacaoDocumentoController : ControllerBase
{
    private readonly ITramitacaoDocumentoService _service;
    private readonly AutoMapper.IMapper _mapper;

    public TramitacaoDocumentoController(ITramitacaoDocumentoService service, AutoMapper.IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DTOs.TramitacaoDocumento.TramitacaoDocumentoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DTOs.TramitacaoDocumento.TramitacaoDocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DTOs.TramitacaoDocumento.TramitacaoDocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<DTOs.TramitacaoDocumento.TramitacaoDocumentoDetailsDto>(entity));
    }

    [Authorize(Policy = AppPolicies.PodeAprovarDocumentos)]
    [HttpPost("{documentoId}/aprovar")]
    public async Task<ActionResult<DocumentoWorkflowResultDto>> Aprovar(
        int documentoId,
        AprovarDocumentoDto dto)
    {
        var result = await _service.AprovarDocumentoAsync(
            documentoId,
            GetUsuarioSistemaId(),
            dto);

        return Ok(result);
    }

    [Authorize(Policy = AppPolicies.PodeAprovarDocumentos)]
    [HttpPost("{documentoId}/rejeitar")]
    public async Task<ActionResult<DocumentoWorkflowResultDto>> Rejeitar(
        int documentoId,
        RejeitarDocumentoDto dto)
    {
        var result = await _service.RejeitarDocumentoAsync(
            documentoId,
            GetUsuarioSistemaId(),
            dto);

        return Ok(result);
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpPost("{documentoId}/encaminhar")]
    public async Task<ActionResult<DocumentoWorkflowResultDto>> Encaminhar(
        int documentoId,
        EncaminharDocumentoDto dto)
    {
        var result = await _service.EncaminharDocumentoAsync(
            documentoId,
            GetUsuarioSistemaId(),
            dto);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DTOs.TramitacaoDocumento.TramitacaoDocumentoDetailsDto>> Post(
        DTOs.TramitacaoDocumento.TramitacaoDocumentoCreateDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Legacy.TramitacaoDocumento>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<DTOs.TramitacaoDocumento.TramitacaoDocumentoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DTOs.TramitacaoDocumento.TramitacaoDocumentoUpdateDto dto)
    {
        var entity = _mapper.Map<Domain.Entities.Legacy.TramitacaoDocumento>(dto);
        var updated = await _service.UpdateAsync(id, entity);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    private int GetUsuarioSistemaId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(claim) || !int.TryParse(claim, out var usuarioId))
        {
            throw new UnauthorizedAccessException("Identificador do utilizador autenticado inválido.");
        }

        return usuarioId;
    }
}
