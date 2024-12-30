using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public interface IAuthorRepository
{
    public AuthorDTO ReadAuthor(Author author);
    
    public Author ReadAuthor(string authorName);
    
    public Task DeleteAuthor(string name);

    public Task Follow(string user, string userToFollow);

    public Task Unfollow(string user, string userToFollow);

    public IEnumerable<AuthorDTO>? GetUserFollowers(string user);

    public IEnumerable<AuthorDTO>? GetFollowersOfUser(string user);
}

