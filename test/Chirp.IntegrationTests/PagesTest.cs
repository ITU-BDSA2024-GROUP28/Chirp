using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Chirp.IntegrationTests;

public class PagesTest : IntegrationTest
{
    
    public PagesTest(WebApplicationFactory<Program> Factory) : base(Factory)
    {
    }

    [Fact]
    public async Task publicTimeline()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        
        var cheep = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cheep);
        Assert.Contains("This is a test cheep from Emma", cheep);
        Assert.Contains( "Welcome to my reality", cheep);
    }
}