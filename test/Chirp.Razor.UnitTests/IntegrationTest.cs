using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Chirp.Razor.Tests;

public class TestAPI : IClassFixture<WebApplicationFactory<Program>>
{
    //private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    //public TestAPI(WebApplicationFactory<Program> fixture)
    //{
    //_fixture = fixture;
    //_client = _fixture.CreateClient(new WebApplicationFactoryClientOptions
    //{ AllowAutoRedirect = true, HandleCookies = true });
    //}

    //[Fact]
    //public async void CanSeePublicTimeline()
    //{
    //var response = await _client.GetAsync("/public");
    //response.EnsureSuccessStatusCode();
    //var content = await response.Content.ReadAsStringAsync();

    //Assert.Contains("Chirp!", content);
    //Assert.Contains("Public Timeline", content);
    //}

    //[Theory]
    //[InlineData("Helge")]
    //[InlineData("Adrian")]
    //public async Task CanSeePrivateTimeline(string author)
    //{
    //var response = await _client.GetAsync($"/{author}");
    //response.EnsureSuccessStatusCode();
    //var content = await response.Content.ReadAsStringAsync();

    //Assert.Contains("Chirp!", content);
    //Assert.Contains($"{author}'s Timeline", content);
    //}
}