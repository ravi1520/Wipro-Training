using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IGenericRepository<Author> _authorRepo;

        public AuthorsController(IGenericRepository<Author> authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepo.GetAllAsync();
            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepo.AddAsync(author);
                return Json(new { success = true, message = "Author created successfully" });
            }
            return Json(new { success = false, message = "Invalid data" });
        }

       
        [HttpPost]
public async Task<IActionResult> Edit(Author author)
{
    if (ModelState.IsValid)
    {
        await _authorRepo.UpdateAsync(author);
        return RedirectToAction("Index");
    }
    return View(author);
}


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorRepo.DeleteAsync(id);
            return Json(new { success = true, message = "Author deleted successfully" });
        }
    }
}
