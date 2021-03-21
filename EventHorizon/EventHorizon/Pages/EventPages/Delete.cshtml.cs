using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.Data.Entities;

namespace EventHorizon.Pages.EventPages
{
    public class DeleteModel : PageModel
    {
        private readonly EventHorizon.Data.DataContext _context;

        public DeleteModel(EventHorizon.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Event
                .Include(x => x.Organizer).FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Event.FindAsync(id);

            if (Event != null)
            {
                _context.Event.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
