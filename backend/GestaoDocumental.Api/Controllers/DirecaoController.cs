using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.Direcao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirecaoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public DirecaoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DirecaoListDto>>> GetAll()
    {
        var entities = await _context.Direcaos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<DirecaoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DirecaoDetailsDto>> GetById(int id)
    {
        var entity = await _context.Direcaos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<DirecaoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DirecaoDetailsDto>> Post(DirecaoCreateDto dto)
    {
        var entity = _mapper.Map<Direcao>(dto);

        _context.Direcaos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<DirecaoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DirecaoUpdateDto dto)
    {
        var entity = await _context.Direcaos.FindAsync(id);

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
        var entity = await _context.Direcaos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Direcaos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
