using Microsoft.EntityFrameworkCore;

namespace mvccore_ajax_demo.Models
{
    public class StudentContext : DbContext
    {
        // ✅ Constructor required for dependency injection
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
