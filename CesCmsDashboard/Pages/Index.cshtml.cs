using CesCmsDashboard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    private readonly IHttpClientFactory _clientFactory;

    public IndexModel(AppDbContext context, IConfiguration config, ILogger<IndexModel> logger, IHttpClientFactory clientFactory)
    {
        _context = context;
        _config = config;
        _logger = logger;
        _clientFactory = clientFactory;
    }

    public int TotalFaqs { get; private set; }
    public int TotalTechTips { get; private set; }
    public bool IsDatabaseConnected { get; private set; }
    public bool IsWebsiteOnline { get; private set; }
    public bool IsApiOnline { get; private set; }
    public List<ActivityItem> RecentActivities { get; set; } = new();

    public async Task OnGetAsync()
    {
        IsDatabaseConnected = await _context.Database.CanConnectAsync();

        TotalFaqs = await _context.Faqs.CountAsync();
        TotalTechTips = await _context.TechTips.CountAsync();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
            AllowAutoRedirect = true
        };
        using var client = new HttpClient(handler);
        client.Timeout = TimeSpan.FromSeconds(5);

        var websiteUrl = _config["SystemStatus:WebsiteUrl"] ?? "https://www.cesitservice.com";
        var apiUrl = _config["SystemStatus:ApiUrl"] ?? "https://test.cesrebuild.com/api/seo/faqs";

        // TODO: Re-evaluate for Production Sprint 2
        /*
        Console.WriteLine($"Starting Ping for: {websiteUrl}");
        try {
            var webResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, websiteUrl));
            IsWebsiteOnline = webResponse.IsSuccessStatusCode;
        } catch (Exception ex) { 
            Console.WriteLine($"PING FAILED | URL: {websiteUrl} | ERROR: {ex.GetType().Name} | MSG: {ex.Message} | INNER: {ex.InnerException?.Message}");
            _logger.LogWarning("Website Ping Failed. Message: {Message}. Inner: {InnerMessage}", ex.Message, ex.InnerException?.Message);
            IsWebsiteOnline = false; 
        }

        Console.WriteLine($"Starting Ping for: {apiUrl}");
        try {
            var apiResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, apiUrl));
            IsApiOnline = apiResponse.IsSuccessStatusCode;
        } catch (Exception ex) { 
            Console.WriteLine($"PING FAILED | URL: {apiUrl} | ERROR: {ex.GetType().Name} | MSG: {ex.Message} | INNER: {ex.InnerException?.Message}");
            _logger.LogWarning("API Ping Failed. Message: {Message}. Inner: {InnerMessage}", ex.Message, ex.InnerException?.Message);
            IsApiOnline = false; 
        }
        */

        var recentLogs = await _context.ActivityLogs
            .OrderByDescending(a => a.Timestamp)
            .Take(5)
            .Select(a => new ActivityItem
            {
                Title = a.EntityTitle,
                Type = a.EntityType,
                Date = a.Timestamp,
                Status = a.ActionType
            })
            .ToListAsync();

        RecentActivities = recentLogs;
    }
}
