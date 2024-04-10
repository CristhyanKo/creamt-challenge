using CreaMT.Communication.Requests;
using CreaMT.Exceptions;
using FluentValidation;

namespace CreaMT.Application.UseCases.Usuario.Update;
public class UpdateUsuarioValidator : AbstractValidator<RequestUpdateUsuarioJson>
{
   public UpdateUsuarioValidator()
    {
        RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage(ResourceMessagesException.NOME_EMPTY);
        RuleFor(usuario => usuario.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(usuario => usuario.Telefone).NotEmpty().WithMessage(ResourceMessagesException.TELEPHONE_EMPTY);
        RuleFor(usuario => usuario.CpfCnpj).NotEmpty().WithMessage(ResourceMessagesException.CPF_CNPJ_EMPTY);
        When(usuario => string.IsNullOrEmpty(usuario.Email) == false, () =>
        {
            RuleFor(usuario => usuario.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });

        When(usuario => string.IsNullOrEmpty(usuario.Telefone) == false, () =>
        {
            RuleFor(usuario => usuario.Telefone).Matches(@"^\d{2}\d{4,5}\d{4}$").WithMessage(ResourceMessagesException.TELEPHONE_INVALID);
        });

        When(usuario => string.IsNullOrEmpty(usuario.CpfCnpj) == false, () =>
        {
            RuleFor(usuario => usuario.CpfCnpj).Must(CpfOuCnpjIsValid).WithMessage(ResourceMessagesException.CPF_CNPJ_INVALID);
        });
    }

    private bool CpfOuCnpjIsValid(string documento)
    {
        return documento.Length >= 11 && documento.Length <= 14 && documento.Length != 12 && documento.Length != 13;
    }
}
