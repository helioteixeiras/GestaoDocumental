using AutoMapper;
using GestaoDocumental.Api.DTOs.Direcao;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DirecaoController : ControllerBase
{
    private readonly IDirecaoService _service;
    private readonly IMapper _mapper;

    public DirecaoController(IDirecaoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DirecaoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DirecaoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DirecaoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<DirecaoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DirecaoDetailsDto>> Post(DirecaoCreateDto dto)
    {
        var entity = _mapper.Map<Direcao>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<DirecaoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DirecaoUpdateDto dto)
    {
        var entity = _mapper.Map<Direcao>(dto);
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
