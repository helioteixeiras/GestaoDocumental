using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;
using GestaoDocumental.Domain.ReadModels;
using GestaoDocumental.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Infrastructure.Data.Repositories;

public class DocumentoWorkflowRepository : IDocumentoWorkflowRepository
{
    private readonly GestaoDocumentalDbContext _context;

    public DocumentoWorkflowRepository(GestaoDocumentalDbContext context)
    {
        _context = context;
    }

    public async Task<Documento?> GetDocumentoAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Documentos
            .Include(documento => documento.EstadoDocumento)
            .FirstOrDefaultAsync(documento => documento.Id == documentoId, cancellationToken);
    }

    public async Task<DocumentoWorkflowReadModel?> GetWorkflowByDocumentoIdAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        var documento = await _context.Documentos
            .Include(item => item.EstadoDocumento)
            .FirstOrDefaultAsync(item => item.Id == documentoId, cancellationToken);

        if (documento is null)
            return null;

        var historicos = await _context.DocumentoHistoricos
            .Include(item => item.Utilizador)
            .Where(item => item.DocumentoId == documentoId)
            .OrderBy(item => item.DataAcao)
            .ThenBy(item => item.Id)
            .ToListAsync(cancellationToken);

        var tramitacoes = await _context.TramitacaoDocumentos
            .Include(item => item.ColaboradorOrigem)
            .Include(item => item.ColaboradorDestino)
            .Include(item => item.DirecaoOrigem)
            .Include(item => item.DirecaoDestino)
            .Where(item => item.DocumentoId == documentoId)
            .OrderBy(item => item.DataEnvio)
            .ThenBy(item => item.Id)
            .ToListAsync(cancellationToken);

        return new DocumentoWorkflowReadModel
        {
            Documento = documento,
            Historicos = historicos,
            Tramitacoes = tramitacoes
        };
    }

    public async Task<UsuarioSistema?> GetUsuarioAsync(
        int usuarioId,
        CancellationToken cancellationToken = default)
    {
        return await _context.UsuarioSistemas
            .Include(usuario => usuario.Perfil)
            .FirstOrDefaultAsync(usuario => usuario.Id == usuarioId, cancellationToken);
    }

    public async Task<EstadoDocumento> EnsureEstadoDocumentoAsync(
        string nome,
        CancellationToken cancellationToken = default)
    {
        var estado = await _context.EstadoDocumentos
            .FirstOrDefaultAsync(item => item.Nome == nome, cancellationToken);

        if (estado is not null)
            return estado;

        estado = new EstadoDocumento
        {
            Nome = nome,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        _context.EstadoDocumentos.Add(estado);
        await _context.SaveChangesAsync(cancellationToken);

        return estado;
    }

    public async Task<TramitacaoDocumento?> GetUltimaTramitacaoAsync(
        int documentoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.TramitacaoDocumentos
            .Where(tramitacao => tramitacao.DocumentoId == documentoId)
            .OrderByDescending(tramitacao => tramitacao.DataEnvio)
            .ThenByDescending(tramitacao => tramitacao.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddTramitacaoAsync(
        TramitacaoDocumento tramitacao,
        CancellationToken cancellationToken = default)
    {
        await _context.TramitacaoDocumentos.AddAsync(tramitacao, cancellationToken);
    }

    public async Task AddHistoricoAsync(
        DocumentoHistorico historico,
        CancellationToken cancellationToken = default)
    {
        await _context.DocumentoHistoricos.AddAsync(historico, cancellationToken);
    }

    public Task UpdateDocumentoAsync(
        Documento documento,
        CancellationToken cancellationToken = default)
    {
        _context.Documentos.Update(documento);
        return Task.CompletedTask;
    }
}
