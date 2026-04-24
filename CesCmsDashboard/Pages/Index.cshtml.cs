using CesCmsDashboard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using System.Text.Json;

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
    private readonly IConfiguration _config;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(AppDbContext context, IConfiguration config, ILogger<IndexModel> logger)
    {
        _context = context;
        _config = config;
        _logger = logger;
        IsAiApiConfigured = !string.IsNullOrEmpty(_config["SEO_API_KEY"]);
    }

    public int TotalFaqs { get; private set; }
    public int TotalTechTips { get; private set; }
    public bool IsDatabaseConnected { get; private set; }
    public bool IsAiApiConfigured { get; set; }
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

    public async Task<JsonResult> OnPostCopilotMessageAsync([FromBody] CopilotRequest request)
    {
        var apiKey = _config["SEO_API_KEY"];

        if (string.IsNullOrEmpty(apiKey))
        {
            _logger.LogWarning("OnPostCopilotMessageAsync: SEO_API_KEY is missing or empty. AI Copilot request aborted.");
            return new JsonResult(new { success = false, reply = "AI Copilot is not configured. Please add SEO_API_KEY to your app settings." });
        }

        try
        {
            var client = new ChatClient("gpt-4o-mini", apiKey);
            var response = await client.CompleteChatAsync(request.Message);

            // TODO: Implement EF Core Chat History Logging here (Save request.Message and response text to DB)
            return new JsonResult(new { success = true, reply = response.Value.Content[0].Text });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to communicate with the OpenAI API during Copilot request.");
            return new JsonResult(new { success = false, reply = "I am currently experiencing technical difficulties connecting to the AI service. Please check the system logs or try again later." });
        }
    }
}

public class CopilotRequest
{
    public string Message { get; set; } = string.Empty;
}
