using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.DocumentoAnexo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentoAnexoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public DocumentoAnexoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoAnexoListDto>>> GetAll()
    {
        var entities = await _context.DocumentoAnexos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoAnexoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoAnexoDetailsDto>> GetById(int id)
    {
        var entity = await _context.DocumentoAnexos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<DocumentoAnexoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DocumentoAnexoDetailsDto>> Post(DocumentoAnexoCreateDto dto)
    {
        var entity = _mapper.Map<DocumentoAnexo>(dto);

        _context.DocumentoAnexos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<DocumentoAnexoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoAnexoUpdateDto dto)
    {
        var entity = await _context.DocumentoAnexos.FindAsync(id);

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
        var entity = await _context.DocumentoAnexos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.DocumentoAnexos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
