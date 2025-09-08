using Microsoft.AspNetCore.Mvc;
using BookstoreApp.Models;
using BookstoreApp.Repository;

namespace BookstoreApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _repo;

        public BookController(BookRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var books = _repo.GetBooks();
            return View(books);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _repo.AddBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _repo.GetBooks().FirstOrDefault(b => b.BookId == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            _repo.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
