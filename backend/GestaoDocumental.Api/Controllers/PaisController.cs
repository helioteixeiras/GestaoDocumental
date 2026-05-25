using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.Pais;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaisController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public PaisController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisListDto>>> GetAll()
    {
        var entities = await _context.Pais.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<PaisListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaisDetailsDto>> GetById(int id)
    {
        var entity = await _context.Pais.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PaisDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<PaisDetailsDto>> Post(PaisCreateDto dto)
    {
        var entity = _mapper.Map<Pais>(dto);

        _context.Pais.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<PaisDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PaisUpdateDto dto)
    {
        var entity = await _context.Pais.FindAsync(id);

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
        var entity = await _context.Pais.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Pais.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
