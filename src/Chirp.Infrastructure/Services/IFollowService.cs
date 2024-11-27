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
    
    public AuthorDTO GetAuthorByName(string name);
    /* From the interface
     * Method to find an author by their name
     * @param string
     * @return AuthorDTO
     */
    
    public List<AuthorDTO> GetFollowing();
    /* From the interface
     * Method to retrive the list of people they are following
     * @param string
     * @return a list of AuthorDTO
     */
    
    protected void Follow(string author);
    
    protected void Unfollow(string author);
    
    public List<CheepDTO> GetCheepsFromFollowing(List<AuthorDTO> authors);
}