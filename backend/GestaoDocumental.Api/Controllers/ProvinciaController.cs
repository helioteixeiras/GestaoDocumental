using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.Provincia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvinciaController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public ProvinciaController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProvinciaListDto>>> GetAll()
    {
        var entities = await _context.Provincia.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ProvinciaListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProvinciaDetailsDto>> GetById(int id)
    {
        var entity = await _context.Provincia.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProvinciaDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<ProvinciaDetailsDto>> Post(ProvinciaCreateDto dto)
    {
        var entity = _mapper.Map<Provincia>(dto);

        _context.Provincia.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<ProvinciaDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProvinciaUpdateDto dto)
    {
        var entity = await _context.Provincia.FindAsync(id);

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
        var entity = await _context.Provincia.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Provincia.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
