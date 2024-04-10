using CreaMT.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CleaMT.CommonTestUtilities.Request;
using CreaMT.Exceptions;
using CreaMT.WebApi.Test.InlineData;
using System.Globalization;

namespace CreaMT.WebApi.Test.Login.DoLoginTest;
public class DoLoginTest : CreaMTClassFixture
{
    private readonly string _method = "login";
    private readonly string _email;
    private readonly string _senha;
    private readonly string _nome;

    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _email = factory.GetEmail();
        _senha = factory.GetPassword();
        _nome = factory.GetName();
    }

    [Fact]
    public async Task DoLoginTest_Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Senha = _senha
        };

        var response = await DoPost(_method, request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_nome);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Logn_Invalid(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();

        var response = await DoPost(_method, request,culture);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

        erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));

    }
}
