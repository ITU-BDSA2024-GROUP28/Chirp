using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.IntegrationTests;
using Chirp.Web;
using Microsoft.AspNetCore.Mvc.Testing;

public class OAuthTest : IClassFixture<IntegrationTest>
{
    private readonly CustomWebApplicationFactory<Program> testFactory;

    public OAuthTest(CustomWebApplicationFactory<Program> factory)
    {
        testFactory = factory;
    }
    
    //Gettting contact to the right girhub profile
    [Fact]
    public async Task GetGithubProfile()
    {
        var client = testFactory.CreateClient();
        
        //put right request Url in for the login path
        var login = "oauth/login";
        
        //put right request Url in for the profile
        var profile = "oauth/profile";
        
        var logRespo = await client.GetAsync();
        logRespo.EnsureSuccessStatusCode();
        
        var profileResp = await client.GetAsync();
        profileResp.EnsureSuccessStatusCode();
        
        Assert.Equal();
        
        Assert.Contains();
    }
}