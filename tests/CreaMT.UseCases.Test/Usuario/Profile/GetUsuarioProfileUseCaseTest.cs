using CleaMT.CommonTestUtilities;
using CleaMT.CommonTestUtilities.Entities;
using CleaMT.CommonTestUtilities.Mapper;
using CleaMT.CommonTestUtilities.Request;
using CreaMT.Application.UseCases.Profile;
using FluentAssertions;
using Xunit;

namespace CreaMT.UseCases.Test.Usuario.Profile;
public class GetUsuarioProfileUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var usuario, var _) = UsuarioBuilder.Build();
        var useCase = CreateUseCase(usuario);
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Nome.Should().Be(usuario.Nome);
        result.Email.Should().Be(usuario.Email);
        result.CpfCnpj.Should().Be(usuario.CpfCnpj);
        result.Telefone.Should().Be(usuario.Telefone);

    }
    private static GetUsuarioProfileUseCase CreateUseCase(CreaMT.Domain.Entities.Usuario usuario)
    {
        var loggedUsuario = LoggedUsuarioBuilder.Build(usuario);
        var mapper = MapperBuilder.Build();
        return new GetUsuarioProfileUseCase(loggedUsuario, mapper);
    }
}
