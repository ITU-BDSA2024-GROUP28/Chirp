using Microsoft.Extensions.DependencyInjection;

namespace Chirp.IntegrationTests;
using Chirp.Web;
using Microsoft.AspNetCore.Mvc.Testing;

public class OAuthTest : IClassFixture<IntegrationTestCustomWeb<Program>>
{
    private readonly IntegrationTestCustomWeb<Program> testFactory;

    public OAuthTest(IntegrationTestCustomWeb<Program> factory)
    {
        testFactory = factory;
    }

    [Fact]
    public async Task GetGithubProfile()
    {
        
    }
}