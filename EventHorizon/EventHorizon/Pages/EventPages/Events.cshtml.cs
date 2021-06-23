using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Data;
using EventHorizon.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventHorizon.Pages.EventPages
{
    public class EventsModel : PageModel
    {
        private readonly EventHorizonContext _context;
        private readonly UserManager<Attendee> _userManager;

        public EventsModel(EventHorizonContext context,
                           UserManager<Attendee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();

        public async Task OnGetAsync()
        {
            Attendee currentUser = null;

            if (User != null)
                currentUser = await _userManager.GetUserAsync(User);

            var tempEvents = _context.Event.ToList();

            foreach (var e in tempEvents)
            {
                var alreadyJoined = currentUser?.JoinedEvents?.FirstOrDefault(x => x.Id.Equals(e.Id)) != null;

                Events.Add(new EventViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    EventImage = e.EventImage,
                    UserAlreadyJoined = alreadyJoined
                });
            }
        }
    }
}
