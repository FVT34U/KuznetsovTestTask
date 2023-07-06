using Microsoft.EntityFrameworkCore;

namespace TestTask;

public class ApplicationContext : DbContext
{
    public DbSet<EngineComponent> EngineComponents => Set<EngineComponent>();
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=KuznetsovTestDatabase;Username=postgres;Password=1234");
    }
}