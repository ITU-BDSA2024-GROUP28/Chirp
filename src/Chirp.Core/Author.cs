using Microsoft.AspNetCore.Identity;

namespace Chirp.Core;

public class Author : IdentityUser<int>
{
    public required ICollection<Cheep> Cheeps { get; set; }
    /*
     * Cheeps retrieves the relevant cheeps from the author and sets them as the collection
     */

    public List<Author> Following { get; set; } = [];

    public List<Author> Followers { get; set; } = [];
}