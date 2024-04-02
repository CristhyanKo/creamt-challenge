using CreaMT.Communication.Requests;
using CreaMT.Communication.Responses;
using CreaMT.Exceptions.ExceptionsBase;
using FluentValidation;

namespace CreaMT.Application.UseCases.Usuario.Register;
public  class RegisterUsuarioUseCase
{
    public ResponseRegisteredUsuariosJson Execute(RequestRegisterUsuarioJson request)
    {
        Validate(request);
        return new ResponseRegisteredUsuariosJson
        {
            Nome = request.Nome,
        };
    }

    public void Validate(RequestRegisterUsuarioJson request)
    {
        var validator = new RegisterUsuarioValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {  
            var errorsMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
