using Chirp.Core;
namespace Chirp.Infrastructure.Services;

public interface IFollowService
{
    public List<CheepDTO> GetCheepsFromAuthor(string author);
    /* From the inteface
     * Method to retrieve cheeps from a specific author, on a specific page
     * @param string, int
     * @return List<CheepDTO>
     */
    
    public List<AuthorDTO> GetFollowing (string username);
    
    public AuthorDTO GetAuthorByName(string name);
    /* From the interface
     * Method to find an author by their name
     * @param string
     * @return AuthorDTO
     */

    public void Follow(string followUsername);
    
    public void Unfollow(string followUsername);
    
    public List<CheepDTO> GetCheepsFromFollowing(List<AuthorDTO> following);
    
    public bool statusFollowing(string username);
}