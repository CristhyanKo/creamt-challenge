using CleaMT.CommonTestUtilities.Request;
using CleaMT.CommonTestUtilities.Tokens;
using CreaMT.Communication.Requests;
using CreaMT.Exceptions;
using CreaMT.WebApi.Test.InlineData;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using Xunit;

namespace CreaMT.WebApi.Test.Usuario.ChangePassword;
public class ChangePasswordTest : CreaMTClassFixture
{
    private readonly string _method = "usuario/change-password";
    private readonly string _senha;
    private readonly string _email;
    private readonly Guid _userIdentifier;

    public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _senha = factory.GetPassword();
        _userIdentifier = factory.GetUserIdentifier();
        _email = factory.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Senha = _senha;
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPut(_method, request, token: token);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);


        var loginRequest = new RequestLoginJson
        {
            Email = _email,
            Senha = _senha,
        };

        var loginResponse = await DoPost("login", loginRequest);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        loginRequest.Senha = request.NovaSenha;

        loginResponse = await DoPost("login", loginRequest);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_NewPassword_Name(string culture)
    {
        var request = new RequestChangePasswordJson
        {
            Senha = _senha,
            NovaSenha = string.Empty
        };

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPut(_method, request, token, culture);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("PASSSWORD_EMPTY", new CultureInfo(culture));

        erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}
