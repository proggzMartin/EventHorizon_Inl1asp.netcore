using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventHorizon.Data;
using EventHorizon.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EventHorizon.Pages.UserPages
{
    public class RegisterModel : PageModel
    {
        private readonly EventHorizonContext _context;
        private readonly UserManager<Attendee> _userManager;


        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public RegisterModel(EventHorizonContext context, 
                             UserManager<Attendee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                //might be unneccesary check, but just in case.
                if (RegisterViewModel == null)
                    ModelState.AddModelError("No input", "Something went wrong, try again");

                else
                {
                    RegisterViewModel.UserName = RegisterViewModel.Email;

                    var result = await _userManager.CreateAsync(RegisterViewModel, RegisterViewModel.Password); //The password is hashed and stored.
                    if (result.Succeeded)
                    {
                        //isPersistent sets if we want to store a session-cookie at user webbrowser or a 'permanent' cookie.
                        //We want session, so isPersistent is set to false.
                        //await _signInManager.SignInAsync(newUser, isPersistent: false); <-- LOGIN men vill göra det separat efteråt.

                        //Skulle kunna använda "tempdata" istället för RedirectToPage med 
                        //dynamiskt objekt.
                        return RedirectToPage(
                            "/Confirmation",
                            "Registration",
                            new { Input = RegisterViewModel.Email }, //https://learningprogramming.net/net/asp-net-core-razor-pages/redirect-to-page-in-asp-net-core-razor-pages/
                            "RegistrationCompleted");
                    }
                    //If there were errors, loop through them.
                    foreach (var error in result.Errors)
                    {
                        //Add them to the modelstate.
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            

            return Page();
        }

        //Convention: OnPostFeedback - Feedback is the same as 'asp-page-handler="Feedback"' in the page.
        public IActionResult OnPostFeedback(string message)
        {
            _context.Feedback.Add(new Feedback() { Text = message });
            _context.SaveChanges();

            return new RedirectToPageResult("/Confirmation", "Feedback");
        }
    }
}
