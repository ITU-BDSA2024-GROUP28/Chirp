using Chirp.Core;

namespace Chirp.Infrastructure.Services;

public interface ICheepService
{
    //retrieve cheeps for a certain page
    public List<CheepDTO> GetCheeps(int? pageNr);
    /* From the interface
     * Method to retrieve get cheeps on a certain page
     * @param int (page number)
     * @return List<CheepDTO>
     */
    
    public List<CheepDTO> GetCheepsFromAuthor(string author, int? pageNr);
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
    
    public AuthorDTO GetAuthorDTOByEmail(string email);
    /* From the interface
     * Method to find an author by their email
     * @param string
     * @return AuthorDTO
     */

    public void CreateCheep(AuthorDTO author, String text, int cheepId);
    /* From the interface
     * Method to create a cheep
     * @param CheepDTO
     */

    public void AddCheep(Cheep cheep);

    public bool MoreCheepsFromAuthor(string authorName, int? pageNr);
    
    public void DeleteAuthor(string name);
    
    public void DeleteCheep(int cheepId);
    public List<CheepDTO> GetCheepsFromAuthors(IEnumerable<string> authors, int page, int pageSize); //Co-authored-by: Mathias <mlao@itu.dk>
    
}