using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.DataLayer;

public class ApplicationContext : DbContext
{
    public ApplicationContext() : base()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Score> Scores => Set<Score>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=backend.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Score>().HasIndex(e => new { e.UserId, e.MaxScore });
        modelBuilder.Entity<Score>().HasIndex(e => new { e.UserId });
                
        modelBuilder.Entity<Score>()
            .HasOne(u => u.User);
        
        modelBuilder.Entity<Score>()
            .HasData(GenerateMockData(111, 3));
        
        modelBuilder.Entity<User>().HasData
        (    
            new User { Id = 1, Name = "Tom"},
            new User { Id = 2, Name = "Alice"},
            new User { Id = 3, Name = "Sam"}, 
            new User { Id = 4, Name = "Bob"}
            );
    }
    
    //Generate MockData
    private Score[] GenerateMockData(int count, int userCount)
    {
        var data = new List<Score>();
        var rand = new Random();
        for (var i = 1; i < count; i++)
            data.Add(new Score()
                { Id = i, UserId = rand.Next(1, userCount), MaxScore = rand.Next(100, 100000)});

        return data.ToArray();
    }
}