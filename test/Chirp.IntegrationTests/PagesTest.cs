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