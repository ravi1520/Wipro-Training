using System;

namespace coreidentity_demo.Models
{
    public class DeactivatedProfile
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateTime DeactivatedOn { get; set; }
        public string Reason { get; set; }

        // Navigation property
        public virtual Profile Profile { get; set; }
    }
}
