using System.ComponentModel.DataAnnotations;

namespace coreidentity_demo.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Please select a role")]
        public string RoleName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number")]
        [Phone] // ✅ This ensures valid phone number format
        public string PhoneNumber { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
