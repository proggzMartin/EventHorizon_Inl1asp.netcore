using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventHorizon.Pages.EventPages
{
    public class DetailsModel : PageModel
    {
        private readonly DataContext _context;

        public Event Event { get; set; }
        public SelectList AttendeeSelect { get; set; }
        public string Message { get; set; }

        [BindProperty]
        public Attendee ChosenAttendee { get; set; }

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

            //Det är ett medvetet dumt val att query:a på eventTitle eftersom den inte är
            //nödvändigtvis unik, men ville testa något nytt
            //(får annan route till sidan)
            Event = await _context.Event
                .Include(x => x.Organizer).FirstOrDefaultAsync(m => m.Id.Equals(id));

            var q = await _context.Attendee.AsNoTracking().ToListAsync();

            AttendeeSelect = new SelectList(q, "Id", "Name");

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            return Page();
        }

    }
}
