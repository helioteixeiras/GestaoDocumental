using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.EstadoDocumento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoDocumentoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public EstadoDocumentoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoDocumentoListDto>>> GetAll()
    {
        var entities = await _context.EstadoDocumentos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<EstadoDocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoDocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _context.EstadoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<EstadoDocumentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<EstadoDocumentoDetailsDto>> Post(EstadoDocumentoCreateDto dto)
    {
        var entity = _mapper.Map<EstadoDocumento>(dto);

        _context.EstadoDocumentos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<EstadoDocumentoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EstadoDocumentoUpdateDto dto)
    {
        var entity = await _context.EstadoDocumentos.FindAsync(id);

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
        var entity = await _context.EstadoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.EstadoDocumentos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
