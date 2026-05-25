using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using GestaoDocumental.Api.DTOs.Documento;
using GestaoDocumental.Application.Common;
using GestaoDocumental.Application.DTOs.Documento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDocumental.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentoController : ControllerBase
{
    private readonly IDocumentoService _service;
    private readonly IMapper _mapper;

    public DocumentoController(IDocumentoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoListDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DocumentoListDto>>(entities));
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id, CancellationToken cancellationToken)
    {
        var result = await _service.DownloadArquivoAsync(id, cancellationToken);

        if (result is null)
            return NotFound();

        return File(result.Content, result.ContentType, result.FileName);
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpPost("{id}/upload")]
    [RequestSizeLimit(10 * 1024 * 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = 10 * 1024 * 1024)]
    public async Task<ActionResult<DocumentoUploadResultDto>> Upload(
        int id,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (file is null)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(DocumentoFileValidator.FileFieldName, "Ficheiro é obrigatório.")
            });
        }

        await using var stream = file.OpenReadStream();
        var result = await _service.UploadArquivoAsync(
            id,
            GetUsuarioSistemaId(),
            file.FileName,
            file.Length,
            stream,
            cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet("{id}/workflow")]
    public async Task<ActionResult<DocumentoWorkflowTimelineDto>> GetWorkflow(int id)
    {
        var result = await _service.ObterWorkflowDocumentoAsync(id);
        return Ok(result);
    }

    [Authorize(Policy = AppPolicies.PodeConsultarDocumentos)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentoDetailsDto>> GetById(int id)
    {
        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(_mapper.Map<DocumentoDetailsDto>(entity));
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpPost]
    public async Task<ActionResult<DocumentoDetailsDto>> Post(DocumentoCreateDto dto)
    {
        var entity = _mapper.Map<Documento>(dto);
        var createdEntity = await _service.CreateAsync(entity);
        var result = _mapper.Map<DocumentoDetailsDto>(createdEntity);

        return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, result);
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DocumentoUpdateDto dto)
    {
        var entity = _mapper.Map<Documento>(dto);
        var updated = await _service.UpdateAsync(id, entity);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    private int GetUsuarioSistemaId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(claim) || !int.TryParse(claim, out var usuarioId))
        {
            throw new UnauthorizedAccessException("Identificador do utilizador autenticado inválido.");
        }

        return usuarioId;
    }
}
