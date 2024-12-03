using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Services;

public class FollowService : IFollowService
{
	ChirpDbContext _context;
	ICheepRepository _repo;
	IAuthorRepository _repoAuthor;
	private List<CheepDTO> _cheeps;
	private List<AuthorDTO> _following;
	public List<CheepDTO> _cheepsFromFollowing;

	public FollowService(ChirpDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
    {
        _context = context;
        _repo = repo;
        _repoAuthor = repoAuthor;
        _following = new List<AuthorDTO>();
        _cheepsFromFollowing = new List<CheepDTO>();
    }


	public List<CheepDTO> GetCheepsFromAuthor(string author)
	{
		var query = (from cheep in _context.Cheeps
				orderby cheep.TimeStamp descending
				select cheep)
			.Include(c => c.Author);
		var result = query.ToList();
        
		// convert the cheep object list to cheepDTO objects
		_cheeps = new List<CheepDTO>();
		var counter = 0;
		
		foreach (Cheep cheep in result)
		{
			if (cheep.Author.UserName == author)
			{
				_cheeps.Add(_repo.ReadCheep(cheep));
				counter++;
			}
		}
		
		return _cheeps;
	}

	public List<AuthorDTO> GetFollowing(string username)
	{
		return _following;
	}

	public AuthorDTO GetAuthorByName(string name)
	{
		var author = _context.Authors.FirstOrDefault(a => a.UserName == name);
		if (author == null)
		{
			throw new ApplicationException("Author not found");
		}
		else
		{
			AuthorDTO authorDto = _repoAuthor.ReadAuthor(author);
			return authorDto;
		}
	}

	public void Follow(string followUsername)
	{
		_following.Add(GetAuthorByName(followUsername));
	}

	public void Unfollow(string followUsername)
	{
		_following.Remove(GetAuthorByName(followUsername));
	}

	public List<CheepDTO> GetCheepsFromFollowing(List<AuthorDTO> following)
	{
		_following = following;
		
		foreach (var author in _following)
		{
			string authorName = author.Name;
			List<CheepDTO> temp = GetCheepsFromAuthor(authorName);
			foreach (var cheep in temp)
			{
				_cheepsFromFollowing.Add(cheep);
			}
		}

		return _cheepsFromFollowing;
	}
	
	public bool FollowingStatus(string username)
	{
		if (_following.Contains(username)){
			return true;
		}
		else 
		{
			return false;
		}
		return null;
	}
}