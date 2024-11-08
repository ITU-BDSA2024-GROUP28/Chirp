using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Chirp.IntegrationTests;

public class PagesTest : IntegrationTest
{
    
    public PagesTest(WebApplicationFactory<Program> Factory) : base(Factory)
    {
    }

    [Fact]
    public async void publicTimeline()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        
        var cont = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cont);
        Assert.Contains("Public Timeline", cont);
    }
    
}