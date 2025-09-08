using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}