using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.Documento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public DocumentoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoListDto>>> GetAll()
    {
        var entities = await _context.Documentos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _context.Documentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<DocumentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DocumentoDetailsDto>> Post(DocumentoCreateDto dto)
    {
        var entity = _mapper.Map<Documento>(dto);

        _context.Documentos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<DocumentoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoUpdateDto dto)
    {
        var entity = await _context.Documentos.FindAsync(id);

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
        var entity = await _context.Documentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Documentos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
