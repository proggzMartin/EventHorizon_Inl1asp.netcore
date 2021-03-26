using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventHorizon.Data;
using EventHorizon.Data.Entities;

namespace EventHorizon.Pages.AttendeePages
{
    public class RegisterModel : PageModel
    {
        private readonly DataContext _context;

        [BindProperty]
        public Attendee Attendee { get; set; }

        public string Message { get; set; }

        public RegisterModel(DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async void OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Message = "Register completed.";

                //_context.Attendee.Add(Attendee);
                //await _context.SaveChangesAsync();

                //return Page();
                //return RedirectToPage("./Index");
            }
        }
    }
}
