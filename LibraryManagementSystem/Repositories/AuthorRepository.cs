using LibraryManagementSystem.Data;    // 👈 add this
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class AuthorRepository : GenericRepository<Author>
    {
        private readonly LibraryContext _context;
        public AuthorRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }
    }
}
