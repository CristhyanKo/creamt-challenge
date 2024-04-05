using AutoMapper;
using CreaMT.Application.Services.Cryptography;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Repositories;
using CreaMT.Domain.Security.Tokens;
using CreaMT.Domain.Services.LoggerUser;
using CreaMT.Communication.Responses;
using CreaMT.Communication.Requests;
using CreaMT.Domain.Extensions;
using CreaMT.Exceptions;
using CreaMT.Exceptions.ExceptionsBase;

namespace CreaMT.Application.UseCases.Usuario.Update;
public class UpdateUsuarioUseCase : IUsuarioUpdateUseCase
{
    private readonly ILoggedUsuario _loggedUsuario;
    private readonly IUsuarioUpdateOnlyRepository _updateOnlyRepository;
    private readonly IUsuarioReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUsuarioUseCase(ILoggedUsuario loggedUsuario,
        IUsuarioUpdateOnlyRepository updateOnlyRepository,
        IUsuarioReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork
        )
    {
        _loggedUsuario = loggedUsuario;
        _updateOnlyRepository = updateOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUsuarioJson request)
    {
        var loggedUsuario = await _loggedUsuario.Usuario();

        await Validate(request, loggedUsuario.Email);

        var usuario = await _updateOnlyRepository.GetById(loggedUsuario.Id);

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.CpfCnpj = request.CpfCnpj;
        usuario.Telefone = request.Telefone;
        usuario.DataAtualizacao = DateTime.UtcNow;

        _updateOnlyRepository.Update(usuario);
        await _unitOfWork.Commit();
    }

    public async Task Validate(RequestUpdateUsuarioJson request, string currentEmail)
    {
        var validator = new UpdateUsuarioValidator();
        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email).IsFalse())
        {
            var userExist = await _readOnlyRepository.ExistActiveUsuarioWithEmail(request.Email);
            if (userExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email",ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }
        }

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
