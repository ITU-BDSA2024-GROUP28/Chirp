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
    
    
    
    [Fact]
    public async Task GetGithubProfile()
    {
        
    }
}