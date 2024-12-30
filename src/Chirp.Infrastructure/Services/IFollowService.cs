using Chirp.Core;
namespace Chirp.Infrastructure.Services;

public interface IFollowService
{
    public List<AuthorDTO> GetFollowing (string username);

    public List<AuthorDTO> GetFollowers(string username);

    public void Follow(string user, string userToFollow);
    
    public void Unfollow(string user, string userToUnfollow);
    
}