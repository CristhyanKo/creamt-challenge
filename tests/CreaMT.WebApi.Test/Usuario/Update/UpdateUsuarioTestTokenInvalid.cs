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
public class UpdateUsuarioTestTokenInvalid : CreaMTClassFixture
{
    private readonly string _method = "usuario";

    public UpdateUsuarioTestTokenInvalid(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        var token = "InvalidoTest";

        var response = await DoPut(_method, request, token: token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);


    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Without_Token(string culture)
    {
        var request = RequestUpdateUsuarioJsonBuilder.Build();
        var token = string.Empty;

        var response = await DoPut(_method, request, token, culture);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

      

    }
}

