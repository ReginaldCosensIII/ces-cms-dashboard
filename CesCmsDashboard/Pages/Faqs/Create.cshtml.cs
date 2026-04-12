using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CesCmsDashboard.Data;
using CesCmsDashboard.Models;

namespace CesCmsDashboard.Pages.Faqs
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Faq Faq { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Faq.Id = Guid.NewGuid();
            Faq.CreatedAt = DateTime.UtcNow;
            Faq.UpdatedAt = DateTime.UtcNow;

            _context.Faqs.Add(Faq);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
