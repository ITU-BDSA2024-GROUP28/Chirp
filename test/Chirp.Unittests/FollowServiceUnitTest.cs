using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Chirp.Infrastructure.Unittests;

public class FollowServiceUnitTest
{
    private ServiceProvider _serviceProvider;
    
    //Setting up the different aspects for testing, it is what happens before each test
    public FollowServiceUnitTest()
    {
        var services = new ServiceCollection();
        
        // use in memory database to test
        services.AddDbContext<ChirpDbContext>(options => options.UseInMemoryDatabase($"InMemoryDatabase_{Guid.NewGuid()}"));
        
        var mockUser = MockUser();
        services.AddSingleton(mockUser);
        
        services.AddScoped<ICheepRepository, CheepRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICheepService, CheepService>();
        services.AddScoped<IFollowService, FollowService>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void GetCheepsFromAuthorTest()
    {
        // Arrange
        using var scope = _serviceProvider.CreateScope();
        {
            // Arrange
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            var followService = scopedServices.GetRequiredService<IFollowService>();

            AddTestCheep(cheepService);

            var result = followService.GetCheepsFromAuthor("Helge");
            
            Assert.NotEmpty(result);
        }
    }

    [Fact]
    public void GetFollwingTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            var followService = scopedServices.GetRequiredService<IFollowService>();
            
            AddTestCheep(cheepService);
            followService.Follow("Helge", "Adrian");

            var result = followService.GetFollowing("Helge");
            
            var follwingAdrian = followService.GetFollowing("Helge")[0];
            
            Assert.NotEmpty(result);
            Assert.Equal("Adrian", follwingAdrian.Name);
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
            Text = "Cheep Test Helge",
            TimeStamp = new DateTime(2000, 1, 1, 15, 50, 40)
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
        
        cheepService.AddCheep(cheep);
        cheepService.AddCheep(cheep2);
        
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