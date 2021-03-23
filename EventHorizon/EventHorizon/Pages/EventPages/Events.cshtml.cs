using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Data;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EventHorizon.Pages.EventPages
{
    public class EventsModel : PageModel
    {
        private readonly DataContext _context;

        public EventsModel(EventHorizon.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get; set; }

        public async Task OnGetAsync()
        {
            Events = await _context.Event
                .Include(x => x.Organizer)
                .ToListAsync();
        }
    }
}
