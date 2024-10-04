namespace Chirp.Razor.IntegrationTests;

using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Chirp.Razor;

/*public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient_client;

    public ChirpRazorIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async void CanSeePublicTimeline()
    {
        var response = await _client.GetAsync("/public");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Chirp!", content);
        Assert.Contains("Public Timeline", content);
    }
    
    
}*/