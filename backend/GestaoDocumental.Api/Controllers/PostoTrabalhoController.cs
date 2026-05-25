using AutoMapper;
using GestaoDocumental.Api.Database;
using GestaoDocumental.Api.Database.Entities;
using GestaoDocumental.Api.DTOs.PostoTrabalho;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostoTrabalhoController : ControllerBase
{
    private readonly GestaoDocumentalDbContext _context;
    private readonly IMapper _mapper;

    public PostoTrabalhoController(GestaoDocumentalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostoTrabalhoListDto>>> GetAll()
    {
        var entities = await _context.PostoTrabalhos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<PostoTrabalhoListDto>>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostoTrabalhoDetailsDto>> GetById(int id)
    {
        var entity = await _context.PostoTrabalhos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PostoTrabalhoDetailsDto>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<PostoTrabalhoDetailsDto>> Post(PostoTrabalhoCreateDto dto)
    {
        var entity = _mapper.Map<PostoTrabalho>(dto);

        _context.PostoTrabalhos.Add(entity);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<PostoTrabalhoDetailsDto>(entity);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PostoTrabalhoUpdateDto dto)
    {
        var entity = await _context.PostoTrabalhos.FindAsync(id);

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
        var entity = await _context.PostoTrabalhos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.PostoTrabalhos.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
