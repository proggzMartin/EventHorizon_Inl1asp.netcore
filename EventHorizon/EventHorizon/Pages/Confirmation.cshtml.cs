
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventHorizon.Pages
{
    public enum ConfirmationType
    {
        Registration,
        Feedback,
        UserUpdated
    }

    public class ConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Input { get; set; }

        public string DisplayMessage { get; set; }

        public ConfirmationType? confType { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetRegistration()
        {
            confType = ConfirmationType.Registration;
            DisplayMessage = $"New user with email <b><u>{Input}</u></b> has been created.";
            return Page();
        }

        public IActionResult OnGetUserUpdated()
        {
            confType = ConfirmationType.UserUpdated;
            DisplayMessage = $"User {Input} was updated.";
            return Page();
        }

        public IActionResult OnGetFeedback()
        {
            confType = ConfirmationType.Feedback;
            return Page();
        }
    }
}
