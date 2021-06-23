using EventHorizon.Utils;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Entities
{
    public class User : IdentityUser //messes up primary key, will have to look at this later.
    {
        //[Key]
        //public int Id { get; set; }

        /* 
            Attributen är till för validation som används vid modelbinding typ; på UserPages/Register, 
            finns en post där dessa kommer till hands och validerar fälten.
            Även i codebehind när ModelState.Valid körs, returnar det true om att nedan attribute's uppfylls.
         */
        [Required, RegularExpression(RegexRules.NameRule, ErrorMessage = RegexRules.NameErrorMessage)]
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        [Required, RegularExpression(RegexRules.NameRule, ErrorMessage = RegexRules.NameErrorMessage)]
        [ProtectedPersonalData]
        public string LastName { get; set; }


        //Username = Email in this application.
        //public override string Email { get => base.Email; set => UserName = value; }
        //public override string UserName { 
        //    get => base.UserName; 
        //    set {
        //        base.UserName = value;
        //        base.Email = value;
        //    }
        //}
        [Phone]
        [ProtectedPersonalData]
        public string Phone { get; set; }

        public ICollection<Event> JoinedEvents { get; set; }
    }
}
