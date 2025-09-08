namespace coreidentity_demo.Models
{
    public class ProfileBadge
    {
        public int ProfileBadgeId { get; set; }

        public int ProfileId { get; set; }
        public int BadgeId { get; set; }

        // Navigation properties
        public virtual Profile Profile { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
