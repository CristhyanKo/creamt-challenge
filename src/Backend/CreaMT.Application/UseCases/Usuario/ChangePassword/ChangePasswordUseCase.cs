using CreaMT.Communication.Requests;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Repositories;
using CreaMT.Domain.Services.LoggerUser;
using CreaMT.Domain.Security.Cryptography;
using CreaMT.Application.UseCases.Usuario.Update;
using CreaMT.Domain.Extensions;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CreaMT.Application.UseCases.Usuario.ChangePassword;
public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUsuario _loggedUsuario;
    private readonly IUsuarioUpdateOnlyRepository _updateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(ILoggedUsuario loggedUsuario,
        IUsuarioUpdateOnlyRepository updateOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter
        )
    {
        _loggedUsuario = loggedUsuario;
        _updateOnlyRepository = updateOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }
    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUsuario = await _loggedUsuario.Usuario();
         Validate(request, loggedUsuario);

        var usuario = await _updateOnlyRepository.GetById(loggedUsuario.Id);

        var newPassword = _passwordEncripter.Encrypt(request.NovaSenha);

        usuario.Senha = newPassword;

        _updateOnlyRepository.Update(usuario);
        await _unitOfWork.Commit();
    }

    public  void Validate(RequestChangePasswordJson request, Domain.Entities.Usuario loggedUsuario)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(request);

        var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Senha);

        if(currentPasswordEncripted.Equals(loggedUsuario.Senha).IsFalse())
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_CURRENT_INVALID));
        }

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
