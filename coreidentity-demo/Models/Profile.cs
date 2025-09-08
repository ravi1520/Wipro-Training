using System.ComponentModel.DataAnnotations;

namespace coreidentity_demo.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        // Computed property (not mapped to DB)
        public string FullName => $"{FirstName} {LastName}".Trim();

        [StringLength(150)]
        public string? TagLine { get; set; }

        public string? About { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? UserName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Url]
        public string? FacebookLink { get; set; }

        [Url]
        public string? TwitterLink { get; set; }

        public DateTime MemberSince { get; set; } = DateTime.UtcNow;

        [Url]
        public string? Website { get; set; }

        public bool ContactVerified { get; set; } = false;

        [Url]
        public string? HeaderImageUrl { get; set; }

        [Url]
        public string? DisplayImageUrl { get; set; }

        [Url]
        public string? ProfileUrl { get; set; }

        // 🔹 Relationship with ProfileBadge
        public virtual ICollection<ProfileBadge>? ProfileBadges { get; set; }
    }
}
