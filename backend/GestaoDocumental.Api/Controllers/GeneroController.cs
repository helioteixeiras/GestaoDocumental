using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.Genero;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeneroController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public GeneroController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GeneroListDto>>> GetAll()
    {
        var entities = await _context.Generos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<GeneroListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GeneroDetailsDto>> GetById(int id)
    {
        var entity = await _context.Generos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<GeneroDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<GeneroDetailsDto>> Post(GeneroCreateDto dto)
    {
        var entity = _mapper.Map<Genero>(dto);

        _context.Generos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<GeneroDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, GeneroUpdateDto dto)
    {
        var entity = await _context.Generos.FindAsync(id);

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
        var entity = await _context.Generos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Generos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
