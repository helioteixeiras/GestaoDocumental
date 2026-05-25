using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.TipoDocumento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoDocumentoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public TipoDocumentoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoDocumentoListDto>>> GetAll()
    {
        var entities = await _context.TipoDocumentos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<TipoDocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TipoDocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _context.TipoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TipoDocumentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TipoDocumentoDetailsDto>> Post(TipoDocumentoCreateDto dto)
    {
        var entity = _mapper.Map<TipoDocumento>(dto);

        _context.TipoDocumentos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<TipoDocumentoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TipoDocumentoUpdateDto dto)
    {
        var entity = await _context.TipoDocumentos.FindAsync(id);

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
        var entity = await _context.TipoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.TipoDocumentos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
