using CleaMT.CommonTestUtilities.Cryptography;
using CleaMT.CommonTestUtilities.Mapper;
using CleaMT.CommonTestUtilities.Repositories;
using CleaMT.CommonTestUtilities.Request;
using CleaMT.CommonTestUtilities.Tokens;
using CreaMT.Application.UseCases.Usuario.Register;
using CreaMT.Domain.Extensions;
using CreaMT.Exceptions;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.UseCases.Test.Usuario.Extensions;
using FluentAssertions;
using Xunit;

namespace CreaMT.UseCases.Test.Usuario.Register;
public class RegisterUsuarioUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();
        var useCase = CreateUseCase();
        var result = await useCase.Execute(request);
        result.Should().NotBeNull();
        result.Tokens.Should().NotBeNull();
        result.Nome.Should().Be(request.Nome);
        result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();
        var useCase = CreateUseCase(TypesOfUsuarioValidations.RepositoryDataEmail, request.Email);

        Func<Task> act = async () => await useCase.Execute(request);
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
    }

    [Fact]
    public async Task Error_CpfCnpj_Already_Registered()
    {
        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();
        var useCase = CreateUseCase(TypesOfUsuarioValidations.RepositoryDataCpfCnpj,request.CpfCnpj);

        Func<Task> act = async () => await useCase.Execute(request);
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.CPF_CNPJ_ALREADY_REGISTERED));
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();
        request.Nome = string.Empty;
        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.NOME_EMPTY));
    }


    private static RegisterUsuarioUseCase CreateUseCase(TypesOfUsuarioValidations validationType = TypesOfUsuarioValidations.RepositoryWithoutConsultation, string? valor = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var writeOnlyRepository = UsuarioWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnlyRepositoryBuilder = new UsuarioReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (valor.NotEmpty() && validationType == TypesOfUsuarioValidations.RepositoryDataEmail)
            readOnlyRepositoryBuilder.ExistActiveUsuarioWithEmail(valor!);

        else if (valor.NotEmpty() && validationType == TypesOfUsuarioValidations.RepositoryDataCpfCnpj)
            readOnlyRepositoryBuilder.ExistActiveUsuarioWithCpfCnpj(valor!);

        return  new RegisterUsuarioUseCase(writeOnlyRepository, readOnlyRepositoryBuilder.Build(), unitOfWork, mapper, passwordEncripter, accessTokenGenerator);
    }
}
