using CesCmsDashboard.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CesCmsDashboard.Pages;

// Lightweight record for the recent activity feed
public record RecentActivityItem(string Title, string Type, DateTime Date);

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    // ── Aggregate Stats ──────────────────────────────────────
    public int TotalAssets    { get; private set; }
    public int PublishedAssets { get; private set; }
    public int DraftAssets    { get; private set; }

    // ── Recent Activity Feed ─────────────────────────────────
    public List<RecentActivityItem> RecentActivity { get; private set; } = new();

    public async Task OnGetAsync()
    {
        // FAQ counts
        var faqTotal     = await _context.Faqs.CountAsync();
        var faqPublished = await _context.Faqs.CountAsync(f => f.IsPublished);

        // TechTip counts
        var tipTotal     = await _context.TechTips.CountAsync();
        var tipPublished = await _context.TechTips.CountAsync(t => t.IsPublished);

        TotalAssets     = faqTotal + tipTotal;
        PublishedAssets = faqPublished + tipPublished;
        DraftAssets     = TotalAssets - PublishedAssets;

        // Gather 5 newest FAQs
        var recentFaqs = await _context.Faqs
            .OrderByDescending(f => f.UpdatedAt)
            .Take(5)
            .Select(f => new RecentActivityItem(f.Question, "FAQ", f.UpdatedAt))
            .ToListAsync();

        // Gather 5 newest TechTips
        var recentTips = await _context.TechTips
            .OrderByDescending(t => t.CreatedAt)
            .Take(5)
            .Select(t => new RecentActivityItem(t.Title, "Tech Tip", t.CreatedAt))
            .ToListAsync();

        // Merge, sort descending, take top 5
        RecentActivity = recentFaqs
            .Concat(recentTips)
            .OrderByDescending(a => a.Date)
            .Take(5)
            .ToList();
    }
}
