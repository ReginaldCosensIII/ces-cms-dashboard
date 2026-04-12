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
    }
}
