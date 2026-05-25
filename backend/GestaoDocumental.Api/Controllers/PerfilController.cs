using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.Perfil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerfilController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public PerfilController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PerfilListDto>>> GetAll()
    {
        var entities = await _context.Perfils.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<PerfilListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PerfilDetailsDto>> GetById(int id)
    {
        var entity = await _context.Perfils.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PerfilDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<PerfilDetailsDto>> Post(PerfilCreateDto dto)
    {
        var entity = _mapper.Map<Perfil>(dto);

        _context.Perfils.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<PerfilDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PerfilUpdateDto dto)
    {
        var entity = await _context.Perfils.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Perfils.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Perfils.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
