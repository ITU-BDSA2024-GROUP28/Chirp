using Microsoft.EntityFrameworkCore;

namespace DBFacade;

public class CheepDbContext : DbContext
{
    public CheepDbContext(DbContextOptions<CheepDbContext> options) : base(options)
    {
    }

    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}