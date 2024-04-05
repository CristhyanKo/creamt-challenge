using CreaMT.Application.SharedValidators;
using CreaMT.Communication.Requests;
using CreaMT.Exceptions;
using FluentValidation;

namespace CreaMT.Application.UseCases.Usuario.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(usuario => usuario.NovaSenha).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }

}
