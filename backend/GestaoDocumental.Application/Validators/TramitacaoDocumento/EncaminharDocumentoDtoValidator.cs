using FluentValidation;
using GestaoDocumental.Application.DTOs.TramitacaoDocumento;

namespace GestaoDocumental.Application.Validators.TramitacaoDocumento;

public class EncaminharDocumentoDtoValidator : AbstractValidator<EncaminharDocumentoDto>
{
    public EncaminharDocumentoDtoValidator()
    {
        RuleFor(x => x.DirecaoDestinoId)
            .GreaterThan(0);

        RuleFor(x => x.ColaboradorDestinoId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Observacao)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Observacao));
    }
}
