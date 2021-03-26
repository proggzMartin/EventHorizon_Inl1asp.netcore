using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventHorizon.Pages.EventPages
{
    public class JoinEvent : PageModel
    {

        public SelectList AttendeeSelect { get; set; }


        public Event Event { get; set; } = new Event();

        [BindProperty]
        public Attendee ChosenAttendee { get; set; } = new Attendee();

        public string Message { get; set; }

        public bool Joined { get; set; }


        private readonly DataContext _context;

        public JoinEvent(DataContext context)
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

        public void OnPost()
        {
            Joined = true;
        }

    }
}
