using FluentValidation;
using GestaoDocumental.Api.DTOs.ClassificacaoDocumento;

namespace GestaoDocumental.Application.Validators.ClassificacaoDocumento;

public class ClassificacaoDocumentoCreateDtoValidator : AbstractValidator<ClassificacaoDocumentoCreateDto>
{
    public ClassificacaoDocumentoCreateDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(200);
    }
}
