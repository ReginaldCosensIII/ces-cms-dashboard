using CesCmsDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace CesCmsDashboard.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Faq> Faqs { get; set; }
}
