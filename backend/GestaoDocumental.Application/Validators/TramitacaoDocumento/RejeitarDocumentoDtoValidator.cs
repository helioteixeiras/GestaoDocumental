using FluentValidation;
using GestaoDocumental.Application.DTOs.TramitacaoDocumento;

namespace GestaoDocumental.Application.Validators.TramitacaoDocumento;

public class RejeitarDocumentoDtoValidator : AbstractValidator<RejeitarDocumentoDto>
{
    public RejeitarDocumentoDtoValidator()
    {
        RuleFor(x => x.Observacao)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(500);
    }
}
