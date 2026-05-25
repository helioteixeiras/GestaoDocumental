using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.ClassificacaoDocumento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassificacaoDocumentoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public ClassificacaoDocumentoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassificacaoDocumentoListDto>>> GetAll()
    {
        var entities = await _context.ClassificacaoDocumentos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ClassificacaoDocumentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassificacaoDocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _context.ClassificacaoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ClassificacaoDocumentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<ClassificacaoDocumentoDetailsDto>> Post(ClassificacaoDocumentoCreateDto dto)
    {
        var entity = _mapper.Map<ClassificacaoDocumento>(dto);

        _context.ClassificacaoDocumentos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<ClassificacaoDocumentoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ClassificacaoDocumentoUpdateDto dto)
    {
        var entity = await _context.ClassificacaoDocumentos.FindAsync(id);

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
        var entity = await _context.ClassificacaoDocumentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.ClassificacaoDocumentos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
