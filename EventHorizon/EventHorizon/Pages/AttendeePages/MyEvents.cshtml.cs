using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.Data.Entities;

namespace EventHorizon.Pages.AttendeePages
{
    public class MyEventsModel : PageModel
    {
        private readonly EventHorizon.Data.DataContext _context;

        public MyEventsModel(EventHorizon.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Attendee> Attendee { get;set; }

        public async Task OnGetAsync()
        {
            var x = _context.Attendee;
            var y = x.ToList();
            Attendee = await _context.Attendee?.ToListAsync();
        }
    }
}
