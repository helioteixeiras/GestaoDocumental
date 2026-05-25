using AutoMapper;
using GestaoDocumental.Api.DTOs.TramitacaoDocumento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TramitacaoDocumentoController : ControllerBase
{
    private readonly ITramitacaoDocumentoService _service;
    private readonly IMapper _mapper;

    public TramitacaoDocumentoController(ITramitacaoDocumentoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TramitacaoDocumentoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<TramitacaoDocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TramitacaoDocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<TramitacaoDocumentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TramitacaoDocumentoDetailsDto>> Post(TramitacaoDocumentoCreateDto dto)
    {
        var entity = _mapper.Map<TramitacaoDocumento>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<TramitacaoDocumentoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TramitacaoDocumentoUpdateDto dto)
    {
        var entity = _mapper.Map<TramitacaoDocumento>(dto);
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
