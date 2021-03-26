using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Entities
{
    public class UserFeedback
    {
        public int Id { get; set; }
        [Required, MinLength(10)]
        public string Feedback { get; set; }
    }
}
