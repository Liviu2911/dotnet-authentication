using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> users { get; set; }
    public DbSet<RefreshToken> refresh_tokens { get; set; }
}