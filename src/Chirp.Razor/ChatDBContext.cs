using Chirp.Razor;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Razor;

public class ChatDbContext : DbContext
{
    private DbSet<Cheep> cheeps { get; set; }
    private DbSet<Author> users { get; set; }

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) {}
}