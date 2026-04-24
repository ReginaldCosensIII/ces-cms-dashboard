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

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<TechTip> TechTips { get;set; } = default!;

        [BindProperty]
        public TechTip NewTechTip { get; set; } = default!;

        [BindProperty]
        public TechTip UpdatedTechTip { get; set; } = default!;

        public async Task OnGetAsync()
        {
            TechTips = await _context.TechTips.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            ModelState.Clear();

            if (!TryValidateModel(NewTechTip, nameof(NewTechTip)))
            {
                ViewData["OpenModal"] = "create-tech-tip-modal";
                TechTips = await _context.TechTips.OrderByDescending(t => t.CreatedAt).ToListAsync();
                return Page();
            }

            NewTechTip.Id = Guid.NewGuid();
            NewTechTip.CreatedAt = DateTime.UtcNow;
            
            _context.TechTips.Add(NewTechTip);
            await _context.SaveChangesAsync();
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var techTip = await _context.TechTips.FindAsync(id);
            if (techTip != null)
            {
                _context.TechTips.Remove(techTip);
                await _context.SaveChangesAsync();
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

            existing.Title = techTip.Title;
            existing.Slug = techTip.Slug;
            existing.Category = techTip.Category;
            existing.VideoUrl = techTip.VideoUrl;
            existing.Content = techTip.Content;
            existing.IsPublished = techTip.IsPublished;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true });
        }
    }
}
