using CleaMT.CommonTestUtilities;
using CleaMT.CommonTestUtilities.Entities;
using CleaMT.CommonTestUtilities.Repositories;
using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Usuario.Update;
using CreaMT.Exceptions;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.UseCases.Test.Usuario.Extensions;
using FluentAssertions;
using Xunit;
using CreaMT.Domain.Extensions;


namespace CreaMT.UseCases.Test.Usuario.Update;
public class UpdateUsuarioUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var usuario, var _) = UsuarioBuilder.Build();
        var request = RequestUpdateUsuarioJsonBuilder.Build();

        var useCase = CreateUseCase(usuario);
        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
        usuario.Nome.Should().Be(request.Nome);
        usuario.Email.Should().Be(request.Email);
        usuario.CpfCnpj.Should().Be(request.CpfCnpj);
        usuario.Telefone.Should().Be(request.Telefone);

    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        (var usuario, var _) = UsuarioBuilder.Build();
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        request.Nome = string.Empty;
        var useCase = CreateUseCase(usuario);

        Func<Task> act = async () => { await useCase.Execute(request); };
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.NOME_EMPTY));

        usuario.Nome.Should().NotBe(request.Nome);
        usuario.Email.Should().NotBe(request.Email);
        usuario.CpfCnpj.Should().NotBe(request.CpfCnpj);
        usuario.Telefone.Should().NotBe(request.Telefone);
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        (var usuario, var _) = UsuarioBuilder.Build();
        var request = RequestUpdateUsuarioJsonBuilder.Build();

        var useCase = CreateUseCase(usuario, TypesOfUsuarioValidations.RepositoryDataEmail, request.Email);

        Func<Task> act = async () => { await useCase.Execute(request); };
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

        usuario.Nome.Should().NotBe(request.Nome);
        usuario.Email.Should().NotBe(request.Email);
        usuario.CpfCnpj.Should().NotBe(request.CpfCnpj);
        usuario.Telefone.Should().NotBe(request.Telefone);
    }

    private static UpdateUsuarioUseCase CreateUseCase(CreaMT.Domain.Entities.Usuario usuario,
        TypesOfUsuarioValidations validationType = TypesOfUsuarioValidations.RepositoryWithoutConsultation,
        string? valor = null)
    {
        var loggedUsuario = LoggedUsuarioBuilder.Build(usuario);
        var userUpdateOnlyRepository = new UsuarioUpdateOnlyRepositoryBuilder().GetById(usuario).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnlyRepositoryBuilder = new UsuarioReadOnlyRepositoryBuilder();

        if (valor.NotEmpty() && validationType == TypesOfUsuarioValidations.RepositoryDataEmail)
            readOnlyRepositoryBuilder.ExistActiveUsuarioWithEmail(valor!);

        return new UpdateUsuarioUseCase(loggedUsuario, userUpdateOnlyRepository, readOnlyRepositoryBuilder.Build(), unitOfWork);
    }
}
