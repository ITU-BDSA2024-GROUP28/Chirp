using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFramework;

namespace Chirp.Infrastructure.Services;

public class FollowService : IFollowService
{
	ChirpDbContext _context;
	ICheepRepository _repo;
	IAuthorRepository _repoAuthor;
	private List<CheepDTO> _cheeps;
	private List<Author> _following;
	public List<CheepDTO> _cheepsFromFollowing;

	public FollowService(ChirpDbContext context, ICheepRepository repe, IAuthorRepository repoAuthor)

    public List<CheepDTO> GetCheepsFromAuthor(string author, int? pageNr);
    
    public List<Author> GetFollowing (string username);

    public void Follow(string author, string followUsername);
    
    public void Unfollow(string author, string followUsername);
    
    public List<CheepDTO> GetCheepsFromFollowing(List<Author> following);
}