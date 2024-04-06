using CleaMT.CommonTestUtilities;
using CleaMT.CommonTestUtilities.Cryptography;
using CleaMT.CommonTestUtilities.Entities;
using CleaMT.CommonTestUtilities.Repositories;
using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Usuario.ChangePassword;
using CreaMT.Application.UseCases.Usuario.Update;
using CreaMT.Communication.Requests;
using CreaMT.Domain.Extensions;
using CreaMT.Exceptions;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.UseCases.Test.Usuario.Extensions;
using FluentAssertions;
using Xunit;

namespace CreaMT.UseCases.Test.Usuario.ChangePassword;
public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var usuario, var senha) = UsuarioBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Senha = senha;

        var useCase = CreateUseCase(usuario);
        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
        
        var passwordEncrypt = PasswordEncripterBuilder.Build();
        usuario.Senha.Should().Be(passwordEncrypt.Encrypt(request.NovaSenha));
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        (var usuario, var senha) = UsuarioBuilder.Build();
        var request = new RequestChangePasswordJson
        {
            Senha = senha,
            NovaSenha = string.Empty
        };

        var useCase = CreateUseCase(usuario);
        Func<Task> act = async () => { await useCase.Execute(request); };
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.PASSSWORD_EMPTY));


        var passwordEncrypt = PasswordEncripterBuilder.Build();
        usuario.Senha.Should().Be(passwordEncrypt.Encrypt(request.Senha));
    }

    [Fact]
    public async Task Error_Current_Password_Different()
    {

        (var usuario, var senha) = UsuarioBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(usuario);

        Func<Task> act = async () => { await useCase.Execute(request); };
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.PASSWORD_CURRENT_INVALID));


        var passwordEncrypt = PasswordEncripterBuilder.Build();
        usuario.Senha.Should().Be(passwordEncrypt.Encrypt(senha));
    }

    private static ChangePasswordUseCase CreateUseCase(CreaMT.Domain.Entities.Usuario usuario,
        TypesOfUsuarioValidations validationType = TypesOfUsuarioValidations.RepositoryWithoutConsultation,
        string? valor = null)
    {
        var loggedUsuario = LoggedUsuarioBuilder.Build(usuario);
        var userUpdateOnlyRepository = new UsuarioUpdateOnlyRepositoryBuilder().GetById(usuario).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncrypt = PasswordEncripterBuilder.Build();

        return new ChangePasswordUseCase(loggedUsuario, userUpdateOnlyRepository, unitOfWork,passwordEncrypt);
    }
}
