using Chirp.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class ChirpDbContext(DbContextOptions<ChirpDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Cheep> Cheeps { get; set; }
    /*
     * Cheeps retrieves the current cheep, then sets it as the cheep
     */
    public DbSet<Author> Authors { get; set; }
    /*
     * Authors retrieves the current set of authors, then sets it as the set of authors
     */
}