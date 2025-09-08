using System.Collections.Generic;

namespace SecureAuthAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public List<string> Roles { get; set; } = new();
    }
}
