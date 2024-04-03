using CreaMT.Communication.Requests;
using CreaMT.Exceptions;
using FluentValidation;

namespace CreaMT.Application.UseCases.Usuario.Register;
public class RegisterUsuarioValidator : AbstractValidator<RequestRegisterUsuarioJson>
{
    public RegisterUsuarioValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage(ResourceMessagesException.NOME_EMPTY);
        RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        RuleFor(x => x.Telefone)
        .NotEmpty().WithMessage(ResourceMessagesException.TELEPHONE_EMPTY)
        .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage(ResourceMessagesException.TELEPHONE_INVALID);
        RuleFor(x => x.CpfCnpj)
            .NotEmpty().WithMessage(ResourceMessagesException.CPF_CNPJ_EMPTY)
            .Must(CpfOuCnpjIsValid).WithMessage(ResourceMessagesException.CPF_CNPJ_INVALID);
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_INVALID);

    }

    private bool CpfOuCnpjIsValid(string documento)
    {
        return documento.Length >= 11 && documento.Length <= 14;
    }
}
