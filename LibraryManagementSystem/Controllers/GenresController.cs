using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenericRepository<Genre> _genreRepo;

        public GenresController(IGenericRepository<Genre> genreRepo)
        {
            _genreRepo = genreRepo;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepo.GetAllAsync();
            return View(genres);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genreRepo.GetByIdAsync(id);
            if (genre == null) return NotFound();
            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepo.AddAsync(genre);
                return Json(new { success = true, message = "Genre created successfully" });
            }
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepo.UpdateAsync(genre);
                return Json(new { success = true, message = "Genre updated successfully" });
            }
            return Json(new { success = false, message = "Update failed" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreRepo.DeleteAsync(id);
            return Json(new { success = true, message = "Genre deleted successfully" });
        }
    }
}
