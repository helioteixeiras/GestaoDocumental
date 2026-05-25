using AutoMapper;
using GestaoDocumental.Infrastructure.Data.Context;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Api.DTOs.Fornecedor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public FornecedorController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FornecedorListDto>>> GetAll()
    {
        var entities = await _context.Fornecedors.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<FornecedorListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FornecedorDetailsDto>> GetById(int id)
    {
        var entity = await _context.Fornecedors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<FornecedorDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorDetailsDto>> Post(FornecedorCreateDto dto)
    {
        var entity = _mapper.Map<Fornecedor>(dto);

        _context.Fornecedors.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<FornecedorDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FornecedorUpdateDto dto)
    {
        var entity = await _context.Fornecedors.FindAsync(id);

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
        var entity = await _context.Fornecedors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.Fornecedors.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
