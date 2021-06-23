using System.ComponentModel.DataAnnotations;

namespace EventHorizon.Data.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
