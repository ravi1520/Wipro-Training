using System.ComponentModel.DataAnnotations;

namespace SecureShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(30, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Min 8, at least 1 upper, 1 number, 1 special
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
            ErrorMessage = "Password must be 8+ chars, include uppercase, number and special char.")]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
