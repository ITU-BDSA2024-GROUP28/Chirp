using Microsoft.AspNetCore.Identity;

namespace Chirp.Core;

public class Author : IdentityUser<int>
{
    public required ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();

    public List<Author> Following { get; set; } = [];

    public List<Author> Followers { get; set; } = [];
}