using CleaMT.CommonTestUtilities.Request;
using CleaMT.CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using Xunit;

namespace CreaMT.WebApi.Test.Usuario.ChangePassword;
public class ChangePasswordInvalidTokenTest : CreaMTClassFixture
{
    private readonly string _method = "usuario/change-password";
    public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        var token = "InvalidoTest";

        var response = await DoPut(_method, request, token: token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);


    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        var token = string.Empty;

        var response = await DoPut(_method, request, token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoPut(_method, request, token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}