using CesCmsDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace CesCmsDashboard.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Faq> Faqs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Faq>().HasData(
            new Faq 
            { 
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                Question = "Local Mock Q1", 
                Answer = "Mock Answer", 
                DisplayOrder = 1, 
                IsPublished = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
