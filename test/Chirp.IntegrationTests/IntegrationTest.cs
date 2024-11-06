using System.Data.Common;
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
                    var dbContext = scopedServices.GetRequiredService<ChirpDbContext>();
                    
                    dbContext.Database.EnsureCreated();
                    
                    SeedDatabase(dbContext);
                }
            }));
        _client = factory.CreateClient();
    }

    public void SeedDatabase(ChirpDbContext context)
    {
        var EmmaTestAuthor = new Author
        {
            Name = "Emma test",
            Email = "emma@test.com",
        };
        
        context.Authors.Add(EmmaTestAuthor);

        var EmmaTestCheep = new Cheep
        {
            Author = EmmaTestAuthor,
            Text = "This is a test cheep from Emma",
            TimeStamp = DateTime.Now,
        };
        
        context.Cheeps.Add(EmmaTestCheep);
        
        var JoseTestAuthor = new Author
        {
            Name = "Jose test",
            Email = "jose@test.com",
        };
        
        context.Authors.Add(JoseTestAuthor);

        var JoseTestCheep = new Cheep
        {
            Author = JoseTestAuthor,
            Text = "Welcome to my reality",
            TimeStamp = DateTime.Now,
        };
        
        context.Cheeps.Add(JoseTestCheep);
        
        context.SaveChanges();
    }
    
}