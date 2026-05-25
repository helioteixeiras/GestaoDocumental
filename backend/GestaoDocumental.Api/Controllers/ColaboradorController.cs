using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.Colaborador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColaboradorController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public ColaboradorController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColaboradorListDto>>> GetAll()
    {
        var entities = await _context.Colaboradors.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ColaboradorListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ColaboradorDetailsDto>> GetById(int id)
    {
        var entity = await _context.Colaboradors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ColaboradorDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<ColaboradorDetailsDto>> Post(ColaboradorCreateDto dto)
    {
        var entity = _mapper.Map<Colaborador>(dto);

        _context.Colaboradors.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<ColaboradorDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ColaboradorUpdateDto dto)
    {
        var entity = await _context.Colaboradors.FindAsync(id);

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
        var entity = await _context.Colaboradors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Colaboradors.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
