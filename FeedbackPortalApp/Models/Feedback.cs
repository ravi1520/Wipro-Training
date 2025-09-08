using System.ComponentModel.DataAnnotations;

namespace FeedbackPortalApp.Models
{
    public class Feedback
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}
