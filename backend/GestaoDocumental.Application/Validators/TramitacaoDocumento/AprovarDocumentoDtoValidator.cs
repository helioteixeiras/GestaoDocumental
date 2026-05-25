using FluentValidation;
using GestaoDocumental.Application.DTOs.TramitacaoDocumento;

namespace GestaoDocumental.Application.Validators.TramitacaoDocumento;

public class AprovarDocumentoDtoValidator : AbstractValidator<AprovarDocumentoDto>
{
    public AprovarDocumentoDtoValidator()
    {
        RuleFor(x => x.Observacao)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Observacao));
    }
}
