using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using BookStoreApi.Models;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books
                                 .Include(b => b.Author)
                                 .ToListAsync();
        }

        // GET: api/books/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                                     .Include(b => b.Author)
                                     .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            return book;
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure referenced author exists
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                return BadRequest(new { message = $"Author with id {book.AuthorId} does not exist." });

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/books/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest(new { message = "Book ID in URL and body do not match." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            // Ensure referenced author exists
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                return BadRequest(new { message = $"Author with id {book.AuthorId} does not exist." });

            // Update fields
            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId;
            existingBook.PublicationYear = book.PublicationYear;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/books/author/2
        [HttpGet("author/{authorId:int}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId)
        {
            var books = await _context.Books
                                      .Where(b => b.AuthorId == authorId)
                                      .Include(b => b.Author)
                                      .ToListAsync();

            if (!books.Any())
                return NotFound(new { message = $"No books found for author with id {authorId}." });

            return books;
        }
    }
}
