using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventHorizon.Pages.UserPages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Attendee ViewUser { get; set; }

        private UserManager<Attendee> _userManager;

        public EditModel(UserManager<Attendee> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
                ModelState.AddModelError("NoEmail", "You reached this page without an email specified, this in not allowed.");
            else
            {
                ViewUser = await _userManager.FindByEmailAsync(Email);

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var targetUser = await _userManager.FindByNameAsync(ViewUser.UserName);
            var result = await _userManager.UpdateAsync(targetUser);

            if(result.Succeeded)
                return RedirectToPage(
                        "/Confirmation",
                        "UserUpdated",
                        new { Input = ViewUser.Email });

            ModelState.AddModelError("Not updated", "Unable to update the user.");
            return Page();
        }
    }
}
