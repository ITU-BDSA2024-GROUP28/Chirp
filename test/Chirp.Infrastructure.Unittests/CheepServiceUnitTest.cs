using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace Chirp.Infrastructure.Unittests;

public class CheapServiceUnitTest
{
    
    private ServiceProvider _serviceProvider;

    public CheapServiceUnitTest()
    {
        SetUp();
    }

    public void SetUp()
    {
        var services = new ServiceCollection();
        
        // use in memory database to test
        services.AddDbContext<ChirpDbContext>(options => options.UseInMemoryDatabase("InMemoryDatabase"));
        
        services.AddScoped<ICheepRepository, CheepRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICheepService, CheepService>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void GetCheepsTest()
    {
        // Arrange
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            List<CheepDTO> cheeps = new List<CheepDTO>();
            cheeps = cheepService.GetCheeps(0);
            Assert.NotEmpty(cheeps);
        }
    }

    [Fact]
    public void GetCheepsFromAuthorTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            List<CheepDTO> authorCheeps = new List<CheepDTO>();
            authorCheeps = cheepService.GetCheepsFromAuthor("Helge", 0);
            Assert.NotEmpty(authorCheeps);
        }
    }
}