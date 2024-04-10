using CreaMT.Exceptions;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaMT.Application.SharedValidators;
internal class PasswordValidator<T> :  PropertyValidator<T,string>
{
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSSWORD_EMPTY);
            return false;
        }
        if(password.Length < 6 )
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_INVALID);
            return false;
        }

        return true;
    }

    public override string Name => "PasswordValidator";
    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
