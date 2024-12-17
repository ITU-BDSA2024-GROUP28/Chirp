using System.Collections.Immutable;
using Chirp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class ChirpDbContext : IdentityDbContext<Author, IdentityRole<int>, int> 
{
    public DbSet<Cheep> Cheeps { get; set; }
        /*
         * Cheeps retrieves the current cheep, then sets it as the cheep
         */
        public DbSet<Author> Authors { get; set; }
        /*
         * Authors retrieves the current set of authors, then sets it as the set of authors
         */
    public ChirpDbContext(DbContextOptions<ChirpDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Author>(entity =>
        {
            entity.HasMany(x => x.Following).WithMany(x => x.Followers);
        });
    }
    /*
     * Function to tell the program that many followings have many followers. A many-to-many relation in the DB
     */
}