using Chirp.Core;
using Chirp.Infrastructure.Repositories;

namespace Chirp.Infrastructure.Services;

public class FollowService : IFollowService
{
	// Dependency injection of author repository
	readonly IAuthorRepository _repoAuthor;
	
	public FollowService(IAuthorRepository repoAuthor)
	{
		_repoAuthor = repoAuthor;
	}

	public List<AuthorDTO> GetFollowing(string username) //Co-authored-by: Mathias <mlao@itu.dk>
	{
		return (_repoAuthor.GetUserFollowers(username) ?? Array.Empty<AuthorDTO>()).ToList();
	}
	
	public List<AuthorDTO> GetFollowers(string username)
	{
		return (_repoAuthor.GetFollowersOfUser(username) ?? Array.Empty<AuthorDTO>()).ToList();
	}

	public void Follow(string user,string followUsername) //Co-authored-by: Mathias <mlao@itu.dk>
	{
		_repoAuthor.Follow(user, followUsername);
	}

	public void Unfollow(string user,string followUsername) //Co-authored-by: Mathias <mlao@itu.dk>
	{
		_repoAuthor.Unfollow(user, followUsername);
	}

}