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
    
    //Testing that if you follow another user we can retrieve the list of the users we are following
    [Fact]
    public void GetFollowingTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            var followService = scopedServices.GetRequiredService<IFollowService>();
            
            AddTestCheep(cheepService);
            followService.Follow("Helge", "Adrian");

            var result = followService.GetFollowing("Helge");
            
            var followingAdrian = followService.GetFollowing("Helge")[0];
            
            Assert.NotEmpty(result);
            Assert.Equal("Adrian", followingAdrian.Name);
        }
    }
    
    //Testing that following and unfollwing works
    [Fact]
    public void UnfollowFollowTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            var followService = scopedServices.GetRequiredService<IFollowService>();
            
            AddTestCheep(cheepService);
            followService.Follow("Helge", "Adrian");
            followService.Follow("Adrian", "Helge");
            
            followService.Unfollow("Helge", "Adrian");
            
            var resultHelge = followService.GetFollowing("Helge");
            var resultAdrian = followService.GetFollowing("Adrian");
            var followingHelge = followService.GetFollowing("Adrian")[0];
            
            Assert.Empty(resultHelge);
            Assert.NotEmpty(resultAdrian);
            Assert.Equal("Helge", followingHelge.Name);
        }
    }
    
    //Testing that we can retrieve the list of the users that are following us
    [Fact]
    public void GetFollowersTest()
    {
        using var scope = _serviceProvider.CreateScope();
        {
            var scopedServices = scope.ServiceProvider;
            var cheepService = scopedServices.GetRequiredService<ICheepService>();
            var followService = scopedServices.GetRequiredService<IFollowService>();
            
            AddTestCheep(cheepService);
            followService.Follow("Adrian", "Helge");
            followService.Follow("Birdy", "Helge");
            
            var result = followService.GetFollowers("Helge");
            var resultAdrian = followService.GetFollowers("Helge")[0];
            var resultBirdy = followService.GetFollowers("Helge")[1];
            
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Adrian", resultAdrian.Name);
            Assert.Equal("Birdy", resultBirdy.Name);
        }
    }
    
    //The testing inputs
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
    
    //Setting up a mockUser for the tests
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