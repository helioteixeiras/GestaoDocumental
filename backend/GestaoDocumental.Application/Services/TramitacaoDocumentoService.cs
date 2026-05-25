using GestaoDocumental.Application.Common;
using GestaoDocumental.Application.DTOs.TramitacaoDocumento;
using GestaoDocumental.Application.Interfaces;
using GestaoDocumental.Domain.Entities.Legacy;
using GestaoDocumental.Domain.Interfaces;

namespace GestaoDocumental.Application.Services;

public class TramitacaoDocumentoService
    : GenericService<TramitacaoDocumento>,
      ITramitacaoDocumentoService
{
    private readonly IDocumentoWorkflowRepository _workflowRepository;

    public TramitacaoDocumentoService(
        IGenericRepository<TramitacaoDocumento> repository,
        IUnitOfWork unitOfWork,
        IDocumentoWorkflowRepository workflowRepository)
        : base(repository, unitOfWork)
    {
        _workflowRepository = workflowRepository;
    }

    public Task<DocumentoWorkflowResultDto> AprovarDocumentoAsync(
        int documentoId,
        int usuarioSistemaId,
        AprovarDocumentoDto request,
        CancellationToken cancellationToken = default) =>
        ProcessarWorkflowAsync(
            documentoId,
            usuarioSistemaId,
            request.Observacao,
            DocumentoWorkflowConstants.EstadoAprovado,
            DocumentoWorkflowConstants.TramitacaoAprovado,
            DocumentoWorkflowConstants.AcaoAprovacao,
            estadoAtualProibido: DocumentoWorkflowConstants.EstadoAprovado,
            requerEncaminhamentoSeRejeitado: true,
            cancellationToken);

    public Task<DocumentoWorkflowResultDto> RejeitarDocumentoAsync(
        int documentoId,
        int usuarioSistemaId,
        RejeitarDocumentoDto request,
        CancellationToken cancellationToken = default) =>
        ProcessarWorkflowAsync(
            documentoId,
            usuarioSistemaId,
            request.Observacao,
            DocumentoWorkflowConstants.EstadoRejeitado,
            DocumentoWorkflowConstants.TramitacaoRejeitado,
            DocumentoWorkflowConstants.AcaoRejeicao,
            estadoAtualProibido: DocumentoWorkflowConstants.EstadoRejeitado,
            requerEncaminhamentoSeRejeitado: false,
            cancellationToken);

    public async Task<DocumentoWorkflowResultDto> EncaminharDocumentoAsync(
        int documentoId,
        int usuarioSistemaId,
        EncaminharDocumentoDto request,
        CancellationToken cancellationToken = default)
    {
        var contexto = await ObterContextoAsync(documentoId, usuarioSistemaId, cancellationToken);
        var estadoAtual = contexto.Documento.EstadoDocumento.Nome;

        if (string.Equals(estadoAtual, DocumentoWorkflowConstants.EstadoAprovado, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Documento aprovado não pode ser encaminhado.");
        }

        if (request.DirecaoDestinoId <= 0)
        {
            throw new InvalidOperationException("Direção de destino é obrigatória.");
        }

        var estadoEmTramitacao = await _workflowRepository.EnsureEstadoDocumentoAsync(
            DocumentoWorkflowConstants.EstadoEmTramitacao,
            cancellationToken);

        var agora = DateTime.UtcNow;
        var tramitacao = CriarTramitacao(
            contexto,
            contexto.Documento.DirecaoOrigemId,
            request.DirecaoDestinoId,
            request.ColaboradorDestinoId,
            DocumentoWorkflowConstants.TramitacaoEncaminhado,
            request.Observacao,
            agora);

        contexto.Documento.EstadoDocumentoId = estadoEmTramitacao.Id;
        contexto.Documento.DataAtualizacao = agora;

        await _workflowRepository.AddTramitacaoAsync(tramitacao, cancellationToken);
        await _workflowRepository.UpdateDocumentoAsync(contexto.Documento, cancellationToken);
        await _workflowRepository.AddHistoricoAsync(
            CriarHistorico(
                contexto,
                DocumentoWorkflowConstants.AcaoEncaminhamento,
                request.Observacao,
                agora),
            cancellationToken);

        await UnitOfWork.SaveChangesAsync();

        return CriarResultado(
            contexto.Documento.Id,
            estadoEmTramitacao.Nome,
            tramitacao,
            DocumentoWorkflowConstants.AcaoEncaminhamento,
            agora,
            request.Observacao);
    }

    private async Task<DocumentoWorkflowResultDto> ProcessarWorkflowAsync(
        int documentoId,
        int usuarioSistemaId,
        string? observacao,
        string novoEstadoDocumento,
        string estadoTramitacao,
        string acaoHistorico,
        string estadoAtualProibido,
        bool requerEncaminhamentoSeRejeitado,
        CancellationToken cancellationToken)
    {
        var contexto = await ObterContextoAsync(documentoId, usuarioSistemaId, cancellationToken);
        var estadoAtual = contexto.Documento.EstadoDocumento.Nome;

        if (string.Equals(estadoAtual, estadoAtualProibido, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Documento já está {estadoAtualProibido.ToLowerInvariant()}.");
        }

        if (requerEncaminhamentoSeRejeitado &&
            string.Equals(estadoAtual, DocumentoWorkflowConstants.EstadoRejeitado, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Documento rejeitado requer encaminhamento antes de nova aprovação.");
        }

        if (string.Equals(estadoAtual, DocumentoWorkflowConstants.EstadoAprovado, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Documento aprovado não permite esta operação.");
        }

        var estadoDestino = await _workflowRepository.EnsureEstadoDocumentoAsync(
            novoEstadoDocumento,
            cancellationToken);

        var agora = DateTime.UtcNow;
        var tramitacao = CriarTramitacao(
            contexto,
            contexto.Documento.DirecaoOrigemId,
            contexto.Documento.DirecaoOrigemId,
            null,
            estadoTramitacao,
            observacao,
            agora);

        contexto.Documento.EstadoDocumentoId = estadoDestino.Id;
        contexto.Documento.DataAtualizacao = agora;

        await _workflowRepository.AddTramitacaoAsync(tramitacao, cancellationToken);
        await _workflowRepository.UpdateDocumentoAsync(contexto.Documento, cancellationToken);
        await _workflowRepository.AddHistoricoAsync(
            CriarHistorico(contexto, acaoHistorico, observacao, agora),
            cancellationToken);

        await UnitOfWork.SaveChangesAsync();

        return CriarResultado(
            contexto.Documento.Id,
            estadoDestino.Nome,
            tramitacao,
            acaoHistorico,
            agora,
            observacao);
    }

    private async Task<WorkflowContext> ObterContextoAsync(
        int documentoId,
        int usuarioSistemaId,
        CancellationToken cancellationToken)
    {
        var documento = await _workflowRepository.GetDocumentoAsync(documentoId, cancellationToken);

        if (documento is null)
        {
            throw new KeyNotFoundException($"Documento '{documentoId}' não encontrado.");
        }

        var usuario = await _workflowRepository.GetUsuarioAsync(usuarioSistemaId, cancellationToken);

        if (usuario is null || !usuario.Ativo || usuario.Bloqueado)
        {
            throw new UnauthorizedAccessException("Utilizador autenticado inválido.");
        }

        return new WorkflowContext(documento, usuario);
    }

    private static TramitacaoDocumento CriarTramitacao(
        WorkflowContext contexto,
        int direcaoOrigemId,
        int direcaoDestinoId,
        int? colaboradorDestinoId,
        string estadoTramitacao,
        string? observacao,
        DateTime agora)
    {
        return new TramitacaoDocumento
        {
            DocumentoId = contexto.Documento.Id,
            DirecaoOrigemId = direcaoOrigemId,
            DirecaoDestinoId = direcaoDestinoId,
            ColaboradorOrigemId = contexto.Usuario.ColaboradorId,
            ColaboradorDestinoId = colaboradorDestinoId,
            Estado = estadoTramitacao,
            Observacao = observacao,
            DataEnvio = agora,
            DataRececao = agora,
            Ativo = true,
            DataCriacao = agora
        };
    }

    private static DocumentoHistorico CriarHistorico(
        WorkflowContext contexto,
        string acao,
        string? observacao,
        DateTime agora)
    {
        var colaboradorHistoricoId = contexto.Usuario.ColaboradorId ?? contexto.Documento.ColaboradorCriadorId;

        return new DocumentoHistorico
        {
            DocumentoId = contexto.Documento.Id,
            UtilizadorId = colaboradorHistoricoId,
            Acao = acao,
            Observacao = observacao,
            DataAcao = agora,
            Ativo = true,
            DataCriacao = agora
        };
    }

    private static DocumentoWorkflowResultDto CriarResultado(
        int documentoId,
        string estadoDocumento,
        TramitacaoDocumento tramitacao,
        string acao,
        DateTime dataAcao,
        string? observacao)
    {
        return new DocumentoWorkflowResultDto
        {
            DocumentoId = documentoId,
            EstadoDocumento = estadoDocumento,
            TramitacaoId = tramitacao.Id,
            Acao = acao,
            DataAcao = dataAcao,
            Observacao = observacao
        };
    }

    private sealed record WorkflowContext(Documento Documento, UsuarioSistema Usuario);
}
