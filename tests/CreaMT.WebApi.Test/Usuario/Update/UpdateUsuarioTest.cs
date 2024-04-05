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

namespace CreaMT.WebApi.Test.Usuario.Update;
public class UpdateUsuarioTest : CreaMTClassFixture
{
    private readonly string _method = "usuario";
    private readonly string _nome;
    private readonly string _email;
    private readonly string _cpfCnpj;
    private readonly string _telefone;
    private readonly Guid _userIdentifier;

    public UpdateUsuarioTest(CustomWebApplicationFactory factory) : base(factory)
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
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPut(_method, request, token: token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        request.Nome = string.Empty;

        var response = await DoPut(_method, request, token, culture);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NOME_EMPTY", new CultureInfo(culture));

        erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}
