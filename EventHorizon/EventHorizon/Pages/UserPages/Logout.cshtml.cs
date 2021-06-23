using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EventHorizon.Pages.UserPages
{
    public class LogoutModel : PageModel
    {
        SignInManager<User> _signInManager;

        public LogoutModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();
            TempData["justLoggedOut"] = true;

            return RedirectToPage("/Index");
        }
    }
}
