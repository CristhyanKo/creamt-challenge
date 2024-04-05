using CleaMT.CommonTestUtilities.Request;
using CleaMT.CommonTestUtilities.Tokens;
using CreaMT.Communication.Requests;
using CreaMT.Exceptions;
using CreaMT.infrastructure.Security.Access.Generator;
using CreaMT.WebApi.Test.InlineData;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using Xunit;

namespace CreaMT.WebApi.Test.Usuario.Profile;
public class GetUsuarioProfileTest : CreaMTClassFixture
{
    private readonly string _method = "usuario";
    private readonly string _nome;
    private readonly string _email;
    private readonly string _cpfCnpj;
    private readonly string _telefone;
    private readonly Guid _userIdentifier;

    public GetUsuarioProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _nome = factory.GetName();
        _email = factory.GetEmail();
        _cpfCnpj = factory.GetCpfCnpj();
        _telefone = factory.GetPhone();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet(_method, token: token);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_nome);
        responseData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_email);
        responseData.RootElement.GetProperty("cpfCnpj").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_cpfCnpj);
        responseData.RootElement.GetProperty("telefone").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_telefone);
    }
}
