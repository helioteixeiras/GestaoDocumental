using AutoMapper;
using GestaoDocumental.Api.DTOs.DocumentoHistorico;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentoHistoricoController : ControllerBase
{
    private readonly IDocumentoHistoricoService _service;
    private readonly IMapper _mapper;

    public DocumentoHistoricoController(IDocumentoHistoricoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoHistoricoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoHistoricoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoHistoricoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<DocumentoHistoricoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DocumentoHistoricoDetailsDto>> Post(DocumentoHistoricoCreateDto dto)
    {
        var entity = _mapper.Map<DocumentoHistorico>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<DocumentoHistoricoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoHistoricoUpdateDto dto)
    {
        var entity = _mapper.Map<DocumentoHistorico>(dto);
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
}
