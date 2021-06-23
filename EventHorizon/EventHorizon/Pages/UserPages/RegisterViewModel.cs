using EventHorizon.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Pages.UserPages
{
    public class RegisterViewModel : User
    {
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Paswords didn't match, try again.")]
        [Display(Name = "Confirm Password")] //display this instead of ConfirmPassword in one word.
        public string ConfirmPassword { get; set; }
    }
}
