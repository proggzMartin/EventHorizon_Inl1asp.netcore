using EventHorizon.Utils;
using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Models
{

    /// <summary>
    /// Right now a copypaste of Attendee, but there's a 1-to-many relation with Event, unlike Attendee.
    /// </summary>
    public class Organizer
    {
        [Key]
        public int Id { get; set; }
        [Required, RegularExpression(RegexRules.NameRule, ErrorMessage = RegexRules.NameErrorMessage)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
