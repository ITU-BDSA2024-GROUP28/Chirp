using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Chirp.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.IntegrationTests;


//Used code from one of the links from lecture https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/test/integration-tests/7.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/IntegrationTests
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var cheepDBCOntextDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<ChirpDbContext>));
            services.Remove(cheepDBCOntextDescriptor);

            var dbConnectionDescriptor =
                services.SingleOrDefault(descriptor => descriptor.ServiceType == typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            services.AddSingleton<DbConnection>(container =>
            {
                var conn = new SqliteConnection("DataSource=:memory:");
                conn.Open();

                return conn;
            });

            services.AddDbContext<ChirpDbContext>((container, options) =>
            {
                var conn = container.GetRequiredService<DbConnection>();
                options.UseSqlite(conn);
            });
        });
        builder.UseEnvironment("Development");
    }
}