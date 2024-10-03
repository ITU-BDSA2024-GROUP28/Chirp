using Chirp.Razor;
using Microsoft.EntityFrameworkCore;

namespace MyChat.Razor;

public class ChatDBContext : DbContext
{
    private DbSet<Message> messages { get; set; }
    private DbSet<User> users { get; set; }

    public ChatDBContext(DbContextOptions<ChatDBContext> options) : base(options) {}
}