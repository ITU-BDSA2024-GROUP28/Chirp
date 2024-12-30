using Chirp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

/// <summary>
/// This class gives EF core information / context about the database
/// </summary>
/// <param name="options"></param>
public class ChirpDbContext(DbContextOptions<ChirpDbContext> options)
    : IdentityDbContext<Author, IdentityRole<int>, int>(options)
{
    public DbSet<Cheep> Cheeps { get; set; }
        
    public DbSet<Author> Authors { get; set; }

    /*
     * Function to tell the program that authors have unique usernames and emails,
     * and an author has many followers and authors they follow. A many-to-many relation in the DB
     */
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Author>().HasIndex(c => c.UserName).IsUnique();
        
        builder.Entity<Author>().HasIndex(c => c.Email).IsUnique();
        
        builder.Entity<Author>().HasMany(x => x.Following).WithMany(x => x.Followers);
    }
    
}