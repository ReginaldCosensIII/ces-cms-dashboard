using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CesCmsDashboard.Data;
using CesCmsDashboard.Models;
using Microsoft.Extensions.Logging;

namespace CesCmsDashboard.Pages.Faqs
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AppDbContext context, IHttpClientFactory clientFactory, ILogger<IndexModel> logger)
        {
            _context = context;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public IList<Faq> Faq { get;set; } = default!;

        [BindProperty]
        public Faq NewFaq { get; set; } = default!;

        [BindProperty]
        public Faq UpdatedFaq { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Faq = await _context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
            
            int nextOrder = await _context.Faqs.AnyAsync() ? await _context.Faqs.MaxAsync(f => f.DisplayOrder) + 1 : 1;
            NewFaq = new Faq { DisplayOrder = nextOrder };
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            // Clear the contaminated state that validated both forms
            ModelState.Clear();

            // Explicitly validate ONLY the NewFaq object
            if (!TryValidateModel(NewFaq, nameof(NewFaq)))
            {
                ViewData["OpenModal"] = "createFaqModal";
                Faq = await _context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
                return Page();
            }

            bool displayOrderExists = await _context.Faqs.AnyAsync(f => f.DisplayOrder == NewFaq.DisplayOrder);
            if (displayOrderExists)
            {
                ViewData["OpenModal"] = "createFaqModal";
                ModelState.AddModelError("NewFaq.DisplayOrder", "This display order number is already in use.");
                Faq = await _context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
                return Page();
            }

            NewFaq.Id = Guid.NewGuid();
            NewFaq.CreatedAt = DateTime.UtcNow;
            
            _context.Faqs.Add(NewFaq);
            _context.ActivityLogs.Add(new ActivityLog { ActionType = "Created", EntityType = "FAQ", EntityTitle = NewFaq.Question, Timestamp = DateTime.UtcNow });
            await _context.SaveChangesAsync();
            
            await FireCacheWebhookAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            // Clear the contaminated state that validated both forms
            ModelState.Clear();

            // Explicitly validate ONLY the UpdatedFaq object
            if (!TryValidateModel(UpdatedFaq, nameof(UpdatedFaq)))
            {
                ViewData["OpenModal"] = "editFaqModal-" + UpdatedFaq.Id;
                Faq = await _context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
                return Page();
            }

            bool displayOrderExists = await _context.Faqs.AnyAsync(f => f.DisplayOrder == UpdatedFaq.DisplayOrder && f.Id != UpdatedFaq.Id);
            if (displayOrderExists)
            {
                ViewData["OpenModal"] = "editFaqModal-" + UpdatedFaq.Id;
                ModelState.AddModelError("UpdatedFaq.DisplayOrder", "This display order number is already in use.");
                Faq = await _context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
                return Page();
            }

            var faq = await _context.Faqs.FindAsync(UpdatedFaq.Id);
            if (faq != null)
            {
                faq.Question = UpdatedFaq.Question;
                faq.Answer = UpdatedFaq.Answer;
                faq.IsPublished = UpdatedFaq.IsPublished;
                faq.DisplayOrder = UpdatedFaq.DisplayOrder;
                faq.UpdatedAt = DateTime.UtcNow;

                _context.ActivityLogs.Add(new ActivityLog { ActionType = "Edited", EntityType = "FAQ", EntityTitle = faq.Question, Timestamp = DateTime.UtcNow });
                await _context.SaveChangesAsync();

                await FireCacheWebhookAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                _context.ActivityLogs.Add(new ActivityLog { ActionType = "Deleted", EntityType = "FAQ", EntityTitle = faq.Question, Timestamp = DateTime.UtcNow });
                await _context.SaveChangesAsync();

                await FireCacheWebhookAsync();
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetEditModalPartialAsync(Guid id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null) return NotFound();
            return Partial("_EditFaqPartial", faq);
        }

        public async Task<IActionResult> OnPostEditAjaxAsync(Faq faq)
        {
            ModelState.Clear();
            if (!TryValidateModel(faq, nameof(Faq)))
            {
                // Return the partial with validation errors baked in
                return Partial("_EditFaqPartial", faq); 
            }

            var existing = await _context.Faqs.FindAsync(faq.Id);
            if (existing == null) return NotFound();

            // Ensure duplicate DisplayOrder checks are handled here if needed, adding to ModelState and returning Partial if failed.
            bool isDuplicate = await _context.Faqs.AnyAsync(f => f.DisplayOrder == faq.DisplayOrder && f.Id != faq.Id);
            if (isDuplicate) {
                ModelState.AddModelError("DisplayOrder", "This display order number is already in use.");
                return Partial("_EditFaqPartial", faq);
            }

            bool hasChanges = false;
            if (existing.Question != faq.Question || 
                existing.Answer != faq.Answer || 
                existing.DisplayOrder != faq.DisplayOrder || 
                existing.IsPublished != faq.IsPublished)
            {
                hasChanges = true;
            }

            if (hasChanges)
            {
                existing.Question = faq.Question;
                existing.Answer = faq.Answer;
                existing.DisplayOrder = faq.DisplayOrder;
                existing.IsPublished = faq.IsPublished;
                existing.UpdatedAt = DateTime.UtcNow;

                _context.ActivityLogs.Add(new ActivityLog { ActionType = "Edited", EntityType = "FAQ", EntityTitle = existing.Question, Timestamp = DateTime.UtcNow });
                await _context.SaveChangesAsync();

                await FireCacheWebhookAsync();
            }
            return new JsonResult(new { success = true });
        }
        private async Task FireCacheWebhookAsync()
        {
            try
            {
                var client = _clientFactory.CreateClient("SeoCacheClient");
                var response = await client.PostAsync("/api/seo/flush-cache", null);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Webhook returned unsuccessful status code: {StatusCode}", response.StatusCode);
                    TempData["WarningMessage"] = "Content saved successfully, but the public website cache could not be instantly updated.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to flush SEO cache via webhook.");
                TempData["WarningMessage"] = "Content saved successfully, but the public website cache could not be instantly updated.";
            }
        }
    }
}
