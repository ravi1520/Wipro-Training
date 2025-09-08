using Microsoft.EntityFrameworkCore;
using BookstoreApp.Models; // Add this line to reference the Book model

namespace BookstoreApp.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } // Reference to BookstoreApp.Models.Book
    }
}
