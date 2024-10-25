using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Core;

public class ChirpDbContext : DbContext
{
    public ChirpDbContext(DbContextOptions<ChirpDbContext> options) : base(options) { }
    
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}