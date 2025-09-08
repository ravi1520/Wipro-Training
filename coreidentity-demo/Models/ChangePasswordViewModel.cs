using System.ComponentModel.DataAnnotations;

namespace coreidentity_demo.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your current password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }   // ✅ added

        [Required(ErrorMessage = "Please enter a new password")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
