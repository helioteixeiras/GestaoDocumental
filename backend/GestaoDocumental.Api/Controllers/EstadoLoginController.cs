using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.EstadoLogin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoLoginController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public EstadoLoginController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoLoginListDto>>> GetAll()
    {
        var entities = await _context.EstadoLogins.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<EstadoLoginListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoLoginDetailsDto>> GetById(int id)
    {
        var entity = await _context.EstadoLogins.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<EstadoLoginDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<EstadoLoginDetailsDto>> Post(EstadoLoginCreateDto dto)
    {
        var entity = _mapper.Map<EstadoLogin>(dto);

        _context.EstadoLogins.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<EstadoLoginDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EstadoLoginUpdateDto dto)
    {
        var entity = await _context.EstadoLogins.FindAsync(id);

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
        var entity = await _context.EstadoLogins.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.EstadoLogins.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
