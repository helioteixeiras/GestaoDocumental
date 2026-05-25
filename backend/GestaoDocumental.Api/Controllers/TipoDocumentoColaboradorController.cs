using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.TipoDocumentoColaborador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoDocumentoColaboradorController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public TipoDocumentoColaboradorController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoDocumentoColaboradorListDto>>> GetAll()
    {
        var entities = await _context.TipoDocumentoColaboradors.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<TipoDocumentoColaboradorListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TipoDocumentoColaboradorDetailsDto>> GetById(int id)
    {
        var entity = await _context.TipoDocumentoColaboradors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TipoDocumentoColaboradorDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TipoDocumentoColaboradorDetailsDto>> Post(TipoDocumentoColaboradorCreateDto dto)
    {
        var entity = _mapper.Map<TipoDocumentoColaborador>(dto);

        _context.TipoDocumentoColaboradors.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<TipoDocumentoColaboradorDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TipoDocumentoColaboradorUpdateDto dto)
    {
        var entity = await _context.TipoDocumentoColaboradors.FindAsync(id);

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
        var entity = await _context.TipoDocumentoColaboradors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.TipoDocumentoColaboradors.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
