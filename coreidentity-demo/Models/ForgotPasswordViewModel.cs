using System.ComponentModel.DataAnnotations;

namespace coreidentity_demo.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }
    }
}
