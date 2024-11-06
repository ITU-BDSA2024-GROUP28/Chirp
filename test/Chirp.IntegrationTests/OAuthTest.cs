using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.IntegrationTests;
using Chirp.Web;
using Microsoft.AspNetCore.Mvc.Testing;

public class OAuthTest : IClassFixture<IntegrationTest>
{
    private readonly WebApplicationFactory<Program> testFactory;

    public OAuthTest(WebApplicationFactory<Program> factory)
    {
        testFactory = factory;
    }
    
    //Gettting contact to the right github profile
    [Fact]
    public async Task GetGithubProfile()
    {
       // var client = testFactory.CreateClient();
        
        //put right request Url in for the login path
        //var login = "oauth/login";
        
        //put right request Url in for the profile
        //var profile = "oauth/profile";
        
        //var logRespo = await client.GetAsync();
        //logRespo.EnsureSuccessStatusCode();
        
        //var profileResp = await client.GetAsync();
        //profileResp.EnsureSuccessStatusCode();
        
        //Assert.Equal();
        
        //Assert.Contains();
    }
}