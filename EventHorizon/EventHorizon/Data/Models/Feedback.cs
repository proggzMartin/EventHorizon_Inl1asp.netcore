using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
