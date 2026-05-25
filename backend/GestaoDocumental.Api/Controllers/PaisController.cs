using AutoMapper;
using GestaoDocumental.Api.DTOs.Pais;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaisController : ControllerBase
{
    private readonly IPaisService _service;
    private readonly IMapper _mapper;

    public PaisController(IPaisService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PaisListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaisDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<PaisDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<PaisDetailsDto>> Post(PaisCreateDto dto)
    {
        var entity = _mapper.Map<Pais>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<PaisDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PaisUpdateDto dto)
    {
        var entity = _mapper.Map<Pais>(dto);
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
