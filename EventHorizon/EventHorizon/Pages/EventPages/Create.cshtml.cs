using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventHorizon.Data;
using EventHorizon.Data.Entities;

namespace EventHorizon.Pages.EventPages
{
    public class CreateModel : PageModel
    {
        private readonly EventHorizon.Data.DataContext _context;

        public CreateModel(EventHorizon.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrganizerId"] = new SelectList(_context.Organizer, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Event.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
