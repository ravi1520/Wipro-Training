using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}
