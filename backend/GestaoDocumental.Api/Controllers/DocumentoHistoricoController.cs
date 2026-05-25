using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.DocumentoHistorico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentoHistoricoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public DocumentoHistoricoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoHistoricoListDto>>> GetAll()
    {
        var entities = await _context.DocumentoHistoricos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoHistoricoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoHistoricoDetailsDto>> GetById(int id)
    {
        var entity = await _context.DocumentoHistoricos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<DocumentoHistoricoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DocumentoHistoricoDetailsDto>> Post(DocumentoHistoricoCreateDto dto)
    {
        var entity = _mapper.Map<DocumentoHistorico>(dto);

        _context.DocumentoHistoricos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<DocumentoHistoricoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoHistoricoUpdateDto dto)
    {
        var entity = await _context.DocumentoHistoricos.FindAsync(id);

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
        var entity = await _context.DocumentoHistoricos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.DocumentoHistoricos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
