using System.Data.Common;
using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.IntegrationTests;

//Used code from one of the links from lecture https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/test/integration-tests/7.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/IntegrationTests
public class CustomWebApplication<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ChirpDbContext>));

            services.Remove(dbContextDescriptor);
                
            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbConnection));
                
            services.Remove(dbConnectionDescriptor);

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                    
                return connection;
            });

            services.AddDbContext<ChirpDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
                
            builder.UseEnvironment("Testing");
        });
    }
    
    public void TestCheeps(ICheepService cheepService)
    {
        var author = new Author
        {
            Id = 1,
            UserName = "Helge",
            Email = "ropf@itu.dk",
            Cheeps = new List<Cheep>()
        };
            
        var cheep = new Cheep()
        {
            CheepId = 1,
            Author = author,
            AuthorId = 1,
            Text = "Cheep Test Helge",
            TimeStamp = new DateTime(2000, 1, 1, 15, 50, 40)
        };

        var cheep2Helge = new Cheep()
        {
            CheepId = 5,
            Author = author,
            AuthorId = 1,
            Text = "Second cheep from Helge",
            TimeStamp = DateTime.Now
        };

        var author2 = new Author()
        {
            Id = 2,
            UserName = "Adrian",
            Email = "adrian@itu.dk",
            Cheeps = new List<Cheep>()
        };

        var cheep2 = new Cheep()
        {
            CheepId = 2,
            Author = author2,
            AuthorId = 2,
            Text = "Cheep Test Adrian",
            TimeStamp = DateTime.Now
        };

        var author3 = new Author()
        {
            Id = 3,
            UserName = "Birdy",
            Email = "birdy@itu.dk",
            Cheeps = new List<Cheep>()
        };

        var cheep3 = new Cheep()
        {
            CheepId = 3,
            Author = author3,
            AuthorId = 3,
            Text = "Cheep Test Birdy",
            TimeStamp = DateTime.Now
        };

        var author4 = new Author()
        {
            Id = 4,
            UserName = "Tweety",
            Email = "tweety@itu.dk",
            Cheeps = new List<Cheep>()
        };

        var cheep4 = new Cheep()
        {
            CheepId = 4,
            Author = author4,
            AuthorId = 4,
            Text = "Cheep Test Tweety",
            TimeStamp = DateTime.Now
        };
        
        cheepService.AddCheep(cheep);
        cheepService.AddCheep(cheep2Helge);
        cheepService.AddCheep(cheep2);
        cheepService.AddCheep(cheep3);
        cheepService.AddCheep(cheep4);
    }
}