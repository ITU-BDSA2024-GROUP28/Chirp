using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Chirp.IntegrationTests;

public class PagesTest : IClassFixture<CustomWebApplication<Program>>
{
    //Used code from one of the links from lecture https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/test/integration-tests/7.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/IntegrationTests
    private readonly HttpClient _client;
    private readonly CustomWebApplication<Program> _factory;
    public PagesTest(CustomWebApplication<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
        
    }
    
    [Fact]
    public async Task publicTimeline()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        
        var cont = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cont);
        Assert.Contains("Public Timeline", cont);
    }
    
    [Fact]
    public async void userTimeline()
    {
        var response = await _client.GetAsync("/EmmaTest");
        response.EnsureSuccessStatusCode();
        
        var cheep = await response.Content.ReadAsStringAsync();
        Assert.Contains("EmmaTest's Timeline", cheep);
        Assert.DoesNotContain("JoseTest's Timeline", cheep);
    }
    
    
}