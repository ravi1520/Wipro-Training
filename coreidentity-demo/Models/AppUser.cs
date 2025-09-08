using Microsoft.AspNetCore.Identity;
namespace coreidentity_demo.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        // Additional properties can be added here
    }
}