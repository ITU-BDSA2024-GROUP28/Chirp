using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using SQLitePCL;

namespace Chirp.Infrastructure.Unittests;

public class CheepServiceUnitTest
{
    
    private ServiceProvider _serviceProvider;
    
    //Setting up the different aspects for testing, it is what happens before each test
    public CheepServiceUnitTest()
    {
        var services = new ServiceCollection();
        
        // use in memory database to test
        services.AddDbContext<ChirpDbContext>(options => options.UseInMemoryDatabase($"InMemoryDatabase_{Guid.NewGuid()}"));
        
        var mockUser = MockUser();
        services.AddSingleton(mockUser);
        
        services.AddScoped<ICheepRepository, CheepRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICheepService, CheepService>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    //Tests that getCheeps works and that there are cheeps in our cheepService
    [Fact]
    public void GetCheepsTest()
    {
        // Arrange
        using var scope = _serviceProvider.CreateScope();
        {
            // Arrange
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();

            AddTestCheep(cheepService);
            // Run method
            var cheeps = cheepService.GetCheeps(0);
            
            // Assert we get come cheeps from initial database
            Assert.NotEmpty(cheeps);
        }
    }
    
    //Tests the method GetCheepsFromAuthor so when you the method it will find cheeps from the author
    [Fact]
    public void GetCheepsFromAuthorTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            AddTestCheep(cheepService);
            
            List<CheepDTO> authorCheeps = new List<CheepDTO>();
            authorCheeps = cheepService.GetCheepsFromAuthor("Helge", 0);
            Assert.NotEmpty(authorCheeps);
        }
    }

    //Tests method GetAuthorsByName rises an exception when you call the method with a name that is not in our database
    [Fact]
    public void GetAuthorsByNameTestNon()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            AddTestCheep(cheepService);

            var exception = Assert.Throws<ApplicationException>(() => cheepService.GetAuthorByName("Nani"));
            
            Assert.Equal("Author not found", exception.Message);
        }
    }
    
    //Tests method GetAuthorsByName gives the right author when you search
    [Fact]
    public void GetAuthorsByNameTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            AddTestCheep(cheepService);
            
            var result = cheepService.GetAuthorByName("Helge");
            
            Assert.NotNull(result);
            Assert.Equal("Helge", result.Name);
        }
    }
    
    //Tests method GetAuthorsByEmail rises an exception when you call the method with a name that is not in our database
    [Fact]
    public void GetAuthorsByEmailTestNon()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            var exception = Assert.Throws<ApplicationException>(() => cheepService.GetAuthorDTOByEmail("Nani"));
            
            Assert.Equal("Author not found", exception.Message);
        }
    }
    
    //Tests method GetAuthorsByName gives the right author when you search
    [Fact]
    public void GetAuthorsByEmailTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            AddTestCheep(cheepService);
            
            var result = cheepService.GetAuthorDTOByEmail("ropf@itu.dk");
            
            Assert.NotNull(result);
            Assert.Equal("ropf@itu.dk", result.Email);
        }
    }
    
    [Fact]
    public void TestTime()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            
            AddTestCheep(cheepService);

            var Cheep = cheepService.GetCheeps(0).First().Timestamp;
            var result = Time.ConvertToString(Cheep);
            
            Assert.Equal("2000-01-01 16.50.40", result);
        }
    }
    
    public void AddTestCheep(ICheepService cheepService)
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
            Text = "Cheep Test",
            TimeStamp = new DateTime(2000, 1, 1, 15, 50, 40)
        };

        cheepService.AddCheep(cheep);
        
    }
    
    private static UserManager<Author> MockUser()
    {
        var store = new Mock<IUserStore<Author>>();
        var mockUserManager = new Mock<UserManager<Author>>(
            store.Object, null, null, null, null, null, null, null, null
        );
        
        mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) => new Author { Email = email, UserName = "TestUser", Cheeps = new List<Cheep>() });

        return mockUserManager.Object;
    }

}