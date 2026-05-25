using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.EstadoColaborador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoColaboradorController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public EstadoColaboradorController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoColaboradorListDto>>> GetAll()
    {
        var entities = await _context.EstadoColaboradors.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<EstadoColaboradorListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoColaboradorDetailsDto>> GetById(int id)
    {
        var entity = await _context.EstadoColaboradors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<EstadoColaboradorDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<EstadoColaboradorDetailsDto>> Post(EstadoColaboradorCreateDto dto)
    {
        var entity = _mapper.Map<EstadoColaborador>(dto);

        _context.EstadoColaboradors.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<EstadoColaboradorDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EstadoColaboradorUpdateDto dto)
    {
        var entity = await _context.EstadoColaboradors.FindAsync(id);

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
        var entity = await _context.EstadoColaboradors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.EstadoColaboradors.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
