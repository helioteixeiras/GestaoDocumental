using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.Departamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public DepartamentoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartamentoListDto>>> GetAll()
    {
        var entities = await _context.Departamentos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<DepartamentoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartamentoDetailsDto>> GetById(int id)
    {
        var entity = await _context.Departamentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<DepartamentoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<DepartamentoDetailsDto>> Post(DepartamentoCreateDto dto)
    {
        var entity = _mapper.Map<Departamento>(dto);

        _context.Departamentos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<DepartamentoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DepartamentoUpdateDto dto)
    {
        var entity = await _context.Departamentos.FindAsync(id);

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
        var entity = await _context.Departamentos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Departamentos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
