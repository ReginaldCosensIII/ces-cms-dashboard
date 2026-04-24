using CesCmsDashboard.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CesCmsDashboard.Pages;

public class ActivityItem
{
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public int TotalFaqs { get; private set; }
    public int TotalTechTips { get; private set; }
    public bool IsDatabaseConnected { get; private set; }
    public List<ActivityItem> RecentActivities { get; set; } = new();

    public async Task OnGetAsync()
    {
        IsDatabaseConnected = await _context.Database.CanConnectAsync();

        TotalFaqs = await _context.Faqs.CountAsync();
        TotalTechTips = await _context.TechTips.CountAsync();

        var recentFaqs = await _context.Faqs
            .Select(f => new ActivityItem
            {
                Title = f.Question,
                Type = "FAQ",
                Date = f.UpdatedAt > DateTime.MinValue ? f.UpdatedAt : f.CreatedAt,
                Status = f.IsPublished ? "Published" : "Draft"
            })
            .ToListAsync();

        var recentTips = await _context.TechTips
            .Select(t => new ActivityItem
            {
                Title = t.Title,
                Type = "Tech Tip",
                Date = t.UpdatedAt.HasValue ? t.UpdatedAt.Value : t.CreatedAt,
                Status = t.IsPublished ? "Published" : "Draft"
            })
            .ToListAsync();

        RecentActivities = recentFaqs
            .Concat(recentTips)
            .OrderByDescending(x => x.Date)
            .Take(10)
            .ToList();
    }
}
