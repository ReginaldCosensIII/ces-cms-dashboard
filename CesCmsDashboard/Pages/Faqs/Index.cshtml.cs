using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CesCmsDashboard.Data;
using CesCmsDashboard.Models;

namespace CesCmsDashboard.Pages.Faqs
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Faq> Faq { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Faq = await _context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync(Faq newFaq)
        {
            if (newFaq == null) return Page();

            newFaq.Id = Guid.NewGuid();
            newFaq.CreatedAt = DateTime.UtcNow;
            
            _context.Faqs.Add(newFaq);
            await _context.SaveChangesAsync();
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(Faq updatedFaq)
        {
            if (updatedFaq == null) return Page();

            var faq = await _context.Faqs.FindAsync(updatedFaq.Id);
            if (faq != null)
            {
                faq.Question = updatedFaq.Question;
                faq.Answer = updatedFaq.Answer;
                faq.IsPublished = updatedFaq.IsPublished;
                faq.DisplayOrder = updatedFaq.DisplayOrder;
                faq.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
