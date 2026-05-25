using AutoMapper;
using GestaoDocumental.Api.DTOs.Documento;
using GestaoDocumental.Application.DTOs.Documento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentoController : ControllerBase
{
    private readonly IDocumentoService _service;
    private readonly IMapper _mapper;

    public DocumentoController(IDocumentoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoListDto>>(entities));
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet("{id}/workflow")]
    public async Task<ActionResult<DocumentoWorkflowTimelineDto>> GetWorkflow(int id)
    {
        var result = await _service.ObterWorkflowDocumentoAsync(id);
        return Ok(result);
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<DocumentoDetailsDto>(entity));
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpPost]
    public async Task<ActionResult<DocumentoDetailsDto>> Post(DocumentoCreateDto dto)
    {
        var entity = _mapper.Map<Documento>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<DocumentoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoUpdateDto dto)
    {
        var entity = _mapper.Map<Documento>(dto);
        var updated = await _service.UpdateAsync(id, entity);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
