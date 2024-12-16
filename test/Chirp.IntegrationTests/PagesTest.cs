using System.Net;
using System.Net.Http.Json;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;


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
        
        _factory.TestCheeps(_factory.Services);
    }
    
    //Test that it displays the public timeline
    [Fact]
    public async Task publicTimeline()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        
        var cont = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cont);
        Assert.Contains("Public Timeline", cont);
        Assert.Contains("Cheep Test Helge", cont);
        Assert.DoesNotContain("Cheep Test Suite", cont);
        Assert.Contains("Cheep Test Birdy", cont);
    }
    
    //Test that if you manually put a user in the url it will redirect to public timeline
    [Fact]
    public async void userTimelineManuallyPutInNotAUser()
    {
        var notAUser = "NotAUser";
        var response = await _client.GetAsync($"/{notAUser}");
        
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        
        var afterRedirect = await _client.GetAsync("/");
        var cont = await afterRedirect.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cont);
        Assert.Contains("Public Timeline", cont);
    }
    
    //Test the usertimeline that a user can be found and that their cheep is displayed
    [Fact]
    public async void userTimeline()
    {
        var user  = "Helge";
        var response = await _client.GetAsync($"/{user}");
        response.EnsureSuccessStatusCode();
        
        var cont = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cont);
        Assert.Contains("Helge's Timeline", cont);
        Assert.Contains("Cheep Test Helge", cont);
    }
    
    //Test the usertimeline that a user can be found and that the message no cheep is displayed
    [Fact]
    public async void noCheepsUserTimeline()
    {
        var user = "Tweety";
        var response = await _client.GetAsync($"/{user}");
        response.EnsureSuccessStatusCode();
        
        var cont = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(cont);
        Assert.DoesNotContain("Cheep Test Helge", cont);
        Assert.Contains("There are no cheeps so far.", cont);
    }
    
    [Fact]
    public async void test()
    {
        var user = "Adrian";
        
        var response = await _client.GetAsync($"/{user}/MyAccount");
        
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.StartsWith($"http://localhost/", response.Headers.Location.ToString());
    }
 
}