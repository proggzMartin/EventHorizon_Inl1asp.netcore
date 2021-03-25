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
    public class DetailsModel : PageModel
    {
        private readonly DataContext _context;

        public Event Event { get; set; }

        public List<Attendee> Attendees { get; set; }

        public DetailsModel(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Event
                .Include(x => x.Organizer).FirstOrDefaultAsync(m => m.Id == id);

            Attendees = await _context.Attendee.AsNoTracking().ToListAsync();

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(int userId)
        {


            return Page();
        }

    }
}
