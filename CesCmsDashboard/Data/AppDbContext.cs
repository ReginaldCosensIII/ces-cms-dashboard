using CesCmsDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace CesCmsDashboard.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Faq> Faqs { get; set; }
    public DbSet<TechTip> TechTips { get; set; }

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
                CreatedAt = new DateTime(2026, 4, 20, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 4, 20, 12, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<TechTip>().HasIndex(t => t.Slug).IsUnique();

        modelBuilder.Entity<TechTip>().HasData(
            new TechTip
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "How to securely share passwords",
                Slug = "how-to-securely-share-passwords",
                Content = "<p>Use a secure password manager to share credentials.</p>",
                IsPublished = false,
                CreatedAt = new DateTime(2026, 4, 20, 12, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
