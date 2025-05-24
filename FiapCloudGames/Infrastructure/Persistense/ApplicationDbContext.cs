using Core.Entity;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistense;
public class ApplicationDbContext : DbContext
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<User> Users { get; set; }

    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        var passwordHasher = new PasswordHasher<object>();
        var hashedPassword = passwordHasher.HashPassword(null, "admin@123");

        modelBuilder.Entity<User>().HasData(
            new User("admin", new Email("admin@admin.com.br"), Password.FromHashed(hashedPassword), Core.Enums.UserType.Administrator)
            {
                Id = -1,
                CreationDate = DateTime.Now,
                IsActive = true
            }
        );

        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Name = "Elden Ring", Description = "A vast action RPG world", Genre = "RPG", Price = 299.99m, IsActive = true, CreationDate = DateTime.Now },
            new Game { Id = 2, Name = "Stardew Valley", Description = "Farming and life simulator", Genre = "Simulation", Price = 39.99m, IsActive = true, CreationDate = DateTime.Now },
            new Game { Id = 3, Name = "Hades", Description = "Roguelike action-packed dungeon crawler", Genre = "Action", Price = 79.99m, IsActive = true, CreationDate = DateTime.Now },
            new Game { Id = 4, Name = "Celeste", Description = "Challenging platformer with a touching story", Genre = "Platformer", Price = 49.99m, IsActive = true, CreationDate = DateTime.Now },
            new Game { Id = 5, Name = "The Witcher 3", Description = "Open-world fantasy RPG with deep narrative", Genre = "RPG", Price = 119.99m, IsActive = true, CreationDate = DateTime.Now }
        );
    }
}
