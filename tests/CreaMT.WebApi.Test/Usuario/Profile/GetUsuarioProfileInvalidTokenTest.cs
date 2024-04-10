using CleaMT.CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using Xunit;

namespace CreaMT.WebApi.Test.Usuario.Profile;
public class GetUsuarioProfileInvalidTokenTest : CreaMTClassFixture
{
    private readonly string _method = "usuario";
    public GetUsuarioProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory) { }
    [Fact]
    public async Task Error_Invalid_Token()
    {
        var response = await DoGet(_method, token: "invalid_token");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Witout_Token()
    {
        var response = await DoGet(_method, token: string.Empty);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
        var response = await DoGet(_method, token: token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
