using Chirp.Razor;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Razor;

public class ChatDbContext : DbContext
{
    public DbSet<Cheep> cheeps { get; set; }
    public DbSet<Author> authors { get; set; }

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) {}
}