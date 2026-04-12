using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CesCmsDashboard.Data;
using CesCmsDashboard.Models;

namespace CesCmsDashboard.Pages.Faqs
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Faq Faq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs.FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }
            Faq = faq;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingFaq = await _context.Faqs.FindAsync(Faq.Id);
            if (existingFaq == null)
            {
                return NotFound();
            }

            existingFaq.Question = Faq.Question;
            existingFaq.Answer = Faq.Answer;
            existingFaq.DisplayOrder = Faq.DisplayOrder;
            existingFaq.IsPublished = Faq.IsPublished;
            existingFaq.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaqExists(Faq.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FaqExists(Guid id)
        {
            return _context.Faqs.Any(e => e.Id == id);
        }
    }
}
