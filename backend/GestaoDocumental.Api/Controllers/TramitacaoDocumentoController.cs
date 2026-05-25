using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.TramitacaoDocumento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TramitacaoDocumentoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public TramitacaoDocumentoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TramitacaoDocumentoListDto>>> GetAll()
    {
        var entities = await _context.TramitacaoDocumentos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<TramitacaoDocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TramitacaoDocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _context.TramitacaoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TramitacaoDocumentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TramitacaoDocumentoDetailsDto>> Post(TramitacaoDocumentoCreateDto dto)
    {
        var entity = _mapper.Map<TramitacaoDocumento>(dto);

        _context.TramitacaoDocumentos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<TramitacaoDocumentoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TramitacaoDocumentoUpdateDto dto)
    {
        var entity = await _context.TramitacaoDocumentos.FindAsync(id);

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
        var entity = await _context.TramitacaoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.TramitacaoDocumentos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
