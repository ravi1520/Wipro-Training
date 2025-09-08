using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace coreidentity_demo.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileBadge> ProfileBadges { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<DeactivatedProfile> DeactivatedProfiles { get; set; }

        // Add other entities here if needed
    }
}
