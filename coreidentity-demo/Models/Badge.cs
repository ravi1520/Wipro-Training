namespace coreidentity_demo.Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }

        // Navigation property
        public virtual ICollection<ProfileBadge>? ProfileBadges { get; set; }
    }
}
