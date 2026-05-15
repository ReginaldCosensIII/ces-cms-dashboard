using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CesCmsDashboard.Data;
using CesCmsDashboard.Models;

namespace CesCmsDashboard.Pages.TechTips
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(AppDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public IList<TechTip> TechTips { get;set; } = default!;

        [BindProperty]
        public TechTip NewTechTip { get; set; } = default!;

        [BindProperty]
        public TechTip UpdatedTechTip { get; set; } = default!;

        public async Task OnGetAsync()
        {
            TechTips = await _context.TechTips.OrderBy(t => t.DisplayOrder).ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            ModelState.Clear();

            if (!TryValidateModel(NewTechTip, nameof(NewTechTip)))
            {
                ViewData["OpenModal"] = "create-tech-tip-modal";
                TechTips = await _context.TechTips.OrderBy(t => t.DisplayOrder).ToListAsync();
                return Page();
            }

            bool displayOrderExists = await _context.TechTips.AnyAsync(t => t.DisplayOrder == NewTechTip.DisplayOrder);
            if (displayOrderExists)
            {
                ViewData["OpenModal"] = "create-tech-tip-modal";
                ModelState.AddModelError("NewTechTip.DisplayOrder", "This display order number is already in use.");
                TechTips = await _context.TechTips.OrderBy(t => t.DisplayOrder).ToListAsync();
                return Page();
            }

            NewTechTip.Id = Guid.NewGuid();
            NewTechTip.CreatedAt = DateTime.UtcNow;
            
            _context.TechTips.Add(NewTechTip);
            _context.ActivityLogs.Add(new ActivityLog { ActionType = "Created", EntityType = "Tech Tip", EntityTitle = NewTechTip.Title, Timestamp = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            FireCacheWebhookAsync();
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var techTip = await _context.TechTips.FindAsync(id);
            if (techTip != null)
            {
                _context.TechTips.Remove(techTip);
                _context.ActivityLogs.Add(new ActivityLog { ActionType = "Deleted", EntityType = "Tech Tip", EntityTitle = techTip.Title, Timestamp = DateTime.UtcNow });
                await _context.SaveChangesAsync();

                FireCacheWebhookAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetEditModalPartialAsync(Guid id)
        {
            var techTip = await _context.TechTips.FindAsync(id);
            if (techTip == null) return NotFound();
            return Partial("_edit-tech-tip-partial", techTip);
        }

        public async Task<IActionResult> OnPostEditAjaxAsync(TechTip techTip)
        {
            ModelState.Clear();
            if (!TryValidateModel(techTip, nameof(TechTip)))
            {
                return Partial("_edit-tech-tip-partial", techTip); 
            }

            var existing = await _context.TechTips.FindAsync(techTip.Id);
            if (existing == null) return NotFound();

            bool isDuplicate = await _context.TechTips.AnyAsync(t => t.DisplayOrder == techTip.DisplayOrder && t.Id != techTip.Id);
            if (isDuplicate) {
                ModelState.AddModelError("DisplayOrder", "This display order number is already in use.");
                return Partial("_edit-tech-tip-partial", techTip);
            }

            bool hasChanges = false;
            if (existing.Title != techTip.Title || 
                existing.Content != techTip.Content || 
                existing.VideoUrl != techTip.VideoUrl || 
                existing.Category != techTip.Category || 
                existing.DisplayOrder != techTip.DisplayOrder ||
                existing.IsPublished != techTip.IsPublished)
            {
                hasChanges = true;
            }

            if (hasChanges)
            {
                existing.Title = techTip.Title;
                existing.Category = techTip.Category;
                existing.VideoUrl = techTip.VideoUrl;
                existing.Content = techTip.Content;
                existing.IsPublished = techTip.IsPublished;
                existing.DisplayOrder = techTip.DisplayOrder;
                existing.UpdatedAt = DateTime.UtcNow;

                _context.ActivityLogs.Add(new ActivityLog { ActionType = "Edited", EntityType = "Tech Tip", EntityTitle = existing.Title, Timestamp = DateTime.UtcNow });
                await _context.SaveChangesAsync();

                FireCacheWebhookAsync();
            }
            return new JsonResult(new { success = true });
        }
        private void FireCacheWebhookAsync()
        {
            try
            {
                var client = _clientFactory.CreateClient("SeoCacheClient");
                _ = client.PostAsync("/seo/flush-cache", null);
            }
            catch (Exception)
            {
                // Suppress exceptions for fire-and-forget
            }
        }
    }
}
