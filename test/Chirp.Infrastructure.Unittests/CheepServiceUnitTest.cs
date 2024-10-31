using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using SQLitePCL;

namespace Chirp.Infrastructure.Unittests;

public class CheapServiceUnitTest
{
    
    private ServiceProvider _serviceProvider;

    public CheapServiceUnitTest()
    {
        SetUp();
    }
    
    //Sætter op vores start til at teste ved brug af inMemory database
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
            // Arrange
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            // Run method
            var cheeps = cheepService.GetCheeps(0);
            
            // Assert we get come cheeps from initial database
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

    [Fact]
    public void GetAuthorsByNameTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();

            var exception = Assert.Throws<ApplicationException>(() => cheepService.GetAuthorByName("Nani"));
            
            Assert.Equal("Author not found", exception.Message);
        }
    }

    [Fact]
    public void GetAuthorsByEmailTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            var result = cheepService.GetAuthorByEmail("ropf@itu.dk");
            
            Assert.NotNull(result);
            Assert.Equal("Helge", result.Name);
            Assert.Equal("ropf@itu.dk", result.Email);
        }
    }
}