using Chirp.Core;
namespace Chirp.Infrastructure.Services;

public interface IFollowService
{
    public List<CheepDTO> GetCheepsFromAuthor(string author, int? pageNr);
    /* From the inteface
     * Method to retrieve cheeps from a specific author, on a specific page
     * @param string, int
     * @return List<CheepDTO>
     */
    
    public List<Author> GetFollowing (string username);

    public void Follow(string author, string followUsername);
    
    public void Unfollow(string author, string followUsername);
    
    public List<CheepDTO> GetCheepsFromFollowing(List<Author> following);
}