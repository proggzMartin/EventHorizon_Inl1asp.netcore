using System.Threading.Tasks;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventHorizon.Pages.UserPages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        SignInManager<User> _signInManager;
        public LoginModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet(string email)
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    LoginViewModel.Email,
                    LoginViewModel.Password,
                    false,
                    false
                );

                if(result.Succeeded)
                {
                    TempData["justLoggedIn"] = true;
                    return Redirect("/Index");
                }

                ModelState.AddModelError("IncorrectPassword", "Email or password was incorrect, please try again.");
            }
            return Page();
        }
    }
}
