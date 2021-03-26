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


        public string ErrorMessage { get; } = "Form contained errors, please correct and try again";
        public bool FormError { get; set; }

        public RegisterModel(DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        //// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Attendee.Add(Attendee);
                _context.SaveChanges();

                return new RedirectToPageResult("/Confirmation", "Registration") ;
            }
            else
            {
                FormError = true;
                return Page();
            }
        }

        //Convention: OnPostFeedback - Feedback is the same as 'asp-page-handler="Feedback"' in the page.
        public IActionResult OnPostFeedback(string message)
        {
            _context.UserFeedback.Add(new UserFeedback() { Feedback = message });
            _context.SaveChanges();

            return new RedirectToPageResult("/Confirmation", "Feedback");
        }
    }
}
