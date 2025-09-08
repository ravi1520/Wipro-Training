using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                                 .Include(b => b.Author)
                                 .Include(b => b.Genre)
                                 .ToListAsync();
        }

        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                                 .Include(b => b.Author)
                                 .Include(b => b.Genre)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
