using CleaMT.CommonTestUtilities.Cryptography;
using CleaMT.CommonTestUtilities.Mapper;
using CleaMT.CommonTestUtilities.Repositories;
using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Login.DoLogin;
using CreaMT.Communication.Requests;
using CreaMT.Exceptions.ExceptionsBase;
using CreaMT.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CleaMT.CommonTestUtilities.Entities;
using CleaMT.CommonTestUtilities.Tokens;

namespace CreaMT.UseCases.Test.Login.DoLogin;
public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var usuario, var password) = UsuarioBuilder.Build();
        var useCase = CreateUseCase(usuario);
        var result = await useCase.Execute(new RequestLoginJson
        {
            Email = usuario.Email,
            Senha = password
        });

        result.Should().NotBeNull();
        result.Tokens.Should().NotBeNull();
        result.Nome.Should().NotBeNullOrWhiteSpace().And.Be(usuario.Nome);
        result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_Usuario()
    {
        var request = RequestLoginJsonBuilder.Build();
        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);
        (await act.Should().ThrowAsync<InvalidLoginException>())
            .Where(e => e.Message.Equals(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID));
    }

    private static DoLoginUseCase CreateUseCase(CreaMT.Domain.Entities.Usuario? usuario = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var UsuarioReadOnlyRepositoryBuilder = new UsuarioReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        if(usuario is not null)
        {
            UsuarioReadOnlyRepositoryBuilder.GetByEmailAndPassword(usuario);
        }

        return new DoLoginUseCase(UsuarioReadOnlyRepositoryBuilder.Build(), passwordEncripter, accessTokenGenerator);
    }
}
