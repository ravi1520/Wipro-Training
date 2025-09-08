using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public bool CanEditTask { get; set; } // Claims-based permission
}
