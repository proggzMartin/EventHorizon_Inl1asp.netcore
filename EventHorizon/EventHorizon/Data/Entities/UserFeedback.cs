using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Entities
{
    public class UserFeedback
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
    }
}
