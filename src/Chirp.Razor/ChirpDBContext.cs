using Chirp.Razor;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Razor;

public class ChirpDBContext : DbContext
{
    public DbSet<Cheep> cheeps { get; set; }
    public DbSet<Author> authors { get; set; }

    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) {}
}