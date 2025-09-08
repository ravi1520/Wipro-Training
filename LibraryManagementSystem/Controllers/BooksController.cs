using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookRepository _bookRepo;
        private readonly IGenericRepository<Author> _authorRepo;
        private readonly IGenericRepository<Genre> _genreRepo;

        public BooksController(BookRepository bookRepo,
                               IGenericRepository<Author> authorRepo,
                               IGenericRepository<Genre> genreRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _genreRepo = genreRepo;
        }

        // GET: /Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepo.GetAllAsync();
            return View(books);
        }

        // GET: /Books/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // GET: /Books/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorRepo.GetAllAsync();
            ViewBag.Genres = await _genreRepo.GetAllAsync();
            return View();
        }

        // POST: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepo.AddAsync(book);
                return RedirectToAction(nameof(Index));
            }

            // reload dropdowns if validation fails
            ViewBag.Authors = await _authorRepo.GetAllAsync();
            ViewBag.Genres = await _genreRepo.GetAllAsync();
            return View(book);
        }

        // GET: /Books/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();

            ViewBag.Authors = await _authorRepo.GetAllAsync();
            ViewBag.Genres = await _genreRepo.GetAllAsync();
            return View(book);
        }

        // POST: /Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepo.UpdateAsync(book);
                return RedirectToAction(nameof(Index));
            }

            // reload dropdowns if validation fails
            ViewBag.Authors = await _authorRepo.GetAllAsync();
            ViewBag.Genres = await _genreRepo.GetAllAsync();
            return View(book);
        }

        // GET: /Books/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // POST: /Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
