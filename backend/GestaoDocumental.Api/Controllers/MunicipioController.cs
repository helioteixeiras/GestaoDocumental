using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.Municipio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MunicipioController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public MunicipioController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MunicipioListDto>>> GetAll()
    {
        var entities = await _context.Municipios.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<MunicipioListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MunicipioDetailsDto>> GetById(int id)
    {
        var entity = await _context.Municipios.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<MunicipioDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<MunicipioDetailsDto>> Post(MunicipioCreateDto dto)
    {
        var entity = _mapper.Map<Municipio>(dto);

        _context.Municipios.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<MunicipioDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, MunicipioUpdateDto dto)
    {
        var entity = await _context.Municipios.FindAsync(id);

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
        var entity = await _context.Municipios.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Municipios.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
