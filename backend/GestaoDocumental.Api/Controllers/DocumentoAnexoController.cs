using AutoMapper;
using GestaoDocumental.Api.DTOs.DocumentoAnexo;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentoAnexoController : ControllerBase
{
    private readonly IDocumentoAnexoService _service;
    private readonly IMapper _mapper;

    public DocumentoAnexoController(IDocumentoAnexoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoAnexoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoAnexoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoAnexoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<DocumentoAnexoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DocumentoAnexoDetailsDto>> Post(DocumentoAnexoCreateDto dto)
    {
        var entity = _mapper.Map<DocumentoAnexo>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<DocumentoAnexoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoAnexoUpdateDto dto)
    {
        var entity = _mapper.Map<DocumentoAnexo>(dto);
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
