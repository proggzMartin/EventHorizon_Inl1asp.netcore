using EventHorizon.Utils;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Models
{
    public class Attendee 
    {
        [Key]
        public int Id { get; set; }
        /* 
            Attributen är till för validation som används vid modelbinding typ; på UserPages/Register, 
            finns en post där dessa kommer till hands och validerar fälten.
            Även i codebehind när ModelState.Valid körs, returnar det true om att nedan attribute's uppfylls.
         */
        [Required, RegularExpression(RegexRules.NameRule, ErrorMessage = RegexRules.NameErrorMessage)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        public ICollection<Event> JoinedEvents { get; set; }
    }
}
