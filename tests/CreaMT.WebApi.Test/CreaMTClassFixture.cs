using Azure.Core;
using System.Net.Http.Json;
using Xunit;

namespace CreaMT.WebApi.Test;
public class CreaMTClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public CreaMTClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async  Task<HttpResponseMessage> DoPost (string method,object request, string culture = "pt")
    {
        ChangeRequestCulture(culture);
        return await _httpClient.PostAsJsonAsync(method, request);
    }

    private void ChangeRequestCulture(string culture)
    {
        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
    }
}
