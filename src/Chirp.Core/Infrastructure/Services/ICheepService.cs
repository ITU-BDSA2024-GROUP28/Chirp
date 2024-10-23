using Chirp.Core;

namespace DomainModel;

public interface ICheepService
{
    //Queries
    
    //retrieve cheeps for a certain page
    public List<CheepDTO> GetCheeps(int? pageNr);
    
    //retrieve cheeps for a certain page that are written by a certain Author who is identified by name
    public List<CheepDTO> GetCheepsFromAuthor(string author, int? pageNr);
    
    //find Author by name
    public AuthorDTO GetAuthorByName(string name);
    
    //find Author by email
    public AuthorDTO GetAuthorByEmail(string email);
    
    
    //Commands
    
    //Create new Author
    public void CreateAuthor(AuthorDTO author);
    
    //Create new Cheep
    public void CreateCheep(CheepDTO cheep);

}