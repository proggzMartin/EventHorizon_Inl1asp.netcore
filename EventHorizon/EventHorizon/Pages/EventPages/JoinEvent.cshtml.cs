using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace EventHorizon.Pages.EventPages
{
    public class JoinEvent : PageModel
    {
        public Event Event { get; set; }

        private readonly EventHorizonContext _context;
        private readonly UserManager<User> _userManager;

        public bool AlreadyJoined { get; set; }
        public bool HaveJoined { get; set; }
        public string Message { get; set; }


        public JoinEvent(EventHorizonContext context,
                         UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Event.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (Event == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var contextUser = await ContextUserJoinedEvents(user.Id);

                if (contextUser.JoinedEvents.Contains(Event))
                {
                    AlreadyJoined = true;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(int eventId)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user!= null)
            {
                var contextUser = await ContextUserJoinedEvents(user.Id);

                if (user != null)
                {
                    contextUser.JoinedEvents.Add(Event);
                    _context.SaveChanges();
                }

                Message = "Successfully joined event!";
            }
            

            return Page();
        }

        private async Task<User> ContextUserJoinedEvents(string id)
        {
            return await _context.User
                    .Include(x => x.JoinedEvents)
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
