using CleaMT.CommonTestUtilities.Request;
using CreaMT.Exceptions;
using CreaMT.WebApi.Test.InlineData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace CreaMT.WebApi.Test.Usuario.Register;
public class RegisterUsuarioTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly string _method = "usuario";

    public RegisterUsuarioTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();


    [Fact]   
    public async Task Success()
    {
        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();
        var response  = await _httpClient.PostAsJsonAsync(_method, request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Nome);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUsuarioJsonBuilder.BuildUserCNPJ();
        request.Nome = string.Empty;

        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

        var response = await _httpClient.PostAsJsonAsync(_method, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NOME_EMPTY", new CultureInfo(culture));

        erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
       
    }
}
