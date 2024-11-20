using System.Data.Common;
using System.Net.Http.Json;
using Chirp.Core;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Chirp.Web;
using Microsoft.Extensions.DependencyInjection;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.IntegrationTests;

//Used code from one of the links from lecture https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/test/integration-tests/7.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/IntegrationTests
public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly WebApplicationFactory<Program> _factory;
    protected readonly HttpClient _client;
    
    public IntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ChirpDbContext>));

                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }
                
                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));
                if (dbConnectionDescriptor != null)
                {
                    services.Remove(dbConnectionDescriptor);
                }
                
                services.AddDbContext<ChirpDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDatabase"));
                
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var cheepService = scopedServices.GetRequiredService<ICheepService>();
                    
                    TestCheeps(cheepService);
                }
            }));
        _client = factory.CreateClient();
    }

    private void TestCheeps(ICheepService cheepService)
    {
        var EmmaTestAuthor = new Author()
        {   
            Id = 1,
            UserName = "Emma",
            Email = "emma@test.com",
            Cheeps = new List<Cheep>(),
        };

        var EmmaTestCheep = new Cheep
        {   
            CheepId = 1,
            Author = EmmaTestAuthor,
            AuthorId = 1,
            Text = "This is a test cheep from Emma",
            TimeStamp = DateTime.Now,
        };
        
        cheepService.AddCheep(EmmaTestCheep);
        
        var JoseTestAuthor = new Author
        {   
            AuthorId = 2,
            Name = "Jose",
            Email = "jose@test.com",
            Cheeps = new List<Cheep>(),
        };

        var JoseTestCheep = new Cheep
        {   
            CheepId = 2,
            Author = JoseTestAuthor,
            AuthorId = 2,
            Text = "Welcome to my reality",
            TimeStamp = DateTime.Now,
        };
        
        cheepService.AddCheep(JoseTestCheep);
    }
    
}