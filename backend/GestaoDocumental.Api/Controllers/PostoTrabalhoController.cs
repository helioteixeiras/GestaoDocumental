using AutoMapper;
using GestaoDocumental.Api.DTOs.PostoTrabalho;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostoTrabalhoController : ControllerBase
{
    private readonly IPostoTrabalhoService _service;
    private readonly IMapper _mapper;

    public PostoTrabalhoController(IPostoTrabalhoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostoTrabalhoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PostoTrabalhoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostoTrabalhoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<PostoTrabalhoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<PostoTrabalhoDetailsDto>> Post(PostoTrabalhoCreateDto dto)
    {
        var entity = _mapper.Map<PostoTrabalho>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<PostoTrabalhoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PostoTrabalhoUpdateDto dto)
    {
        var entity = _mapper.Map<PostoTrabalho>(dto);
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
