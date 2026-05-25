using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.UsuarioSistema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioSistemaController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioSistemaController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioSistemaListDto>>> GetAll()
    {
        var entities = await _context.UsuarioSistemas.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<UsuarioSistemaListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioSistemaDetailsDto>> GetById(int id)
    {
        var entity = await _context.UsuarioSistemas.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UsuarioSistemaDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioSistemaDetailsDto>> Post(UsuarioSistemaCreateDto dto)
    {
        var entity = _mapper.Map<UsuarioSistema>(dto);

        _context.UsuarioSistemas.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<UsuarioSistemaDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UsuarioSistemaUpdateDto dto)
    {
        var entity = await _context.UsuarioSistemas.FindAsync(id);

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
        var entity = await _context.UsuarioSistemas.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.UsuarioSistemas.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
