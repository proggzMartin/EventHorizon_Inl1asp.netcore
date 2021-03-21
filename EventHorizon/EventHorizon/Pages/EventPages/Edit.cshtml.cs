using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.Data.Entities;

namespace EventHorizon.Pages.EventPages
{
    public class EditModel : PageModel
    {
        private readonly EventHorizon.Data.DataContext _context;

        public EditModel(EventHorizon.Data.DataContext context)
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
           ViewData["OrganizerId"] = new SelectList(_context.Organizer, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.Id))
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

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
