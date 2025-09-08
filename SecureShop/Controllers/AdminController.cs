using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Data;

namespace SecureShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Dashboard()
        {
            var stats = new
            {
                Products = await _db.Products.CountAsync(),
                Reviews = await _db.Reviews.CountAsync(),
                Users = 0 // add count by injecting UserManager if you want
            };
            return View(stats);
        }
    }
}
