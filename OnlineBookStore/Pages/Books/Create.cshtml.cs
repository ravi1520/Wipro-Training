using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Data;
using OnlineBookStore.Models;

namespace OnlineBookStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public Book Book { get; set; }

        public CreateModel(AppDbContext context) => _context = context;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _context.Books.Add(Book);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
