using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Data;
using SecureShop.Models;
using SecureShop.ViewModels;

namespace SecureShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db) 
        { 
            _db = db; 
        }

        // Public - list all products
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.AsNoTracking().ToListAsync();
            return View(products);
        }

        // Details - show product + reviews
        public async Task<IActionResult> Details(int? id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            var reviews = await _db.Reviews
                .Where(r => r.ProductId == id)
                .AsNoTracking()
                .ToListAsync();

            var model = new ProductDetailsViewModel
            {
                Product = product,
                Reviews = reviews
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult CreateReview(int productId) =>
            View(new ReviewCreateViewModel { ProductId = productId });

        [HttpPost]
        [Authorize(Roles = "Customer,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(ReviewCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var review = new Review
            {
                ProductId = vm.ProductId,
                Username = vm.Username,
                Content = vm.Content // Razor will encode automatically
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", new { id = vm.ProductId });
        }

        // Example of safe parameterized raw SQL
        [NonAction]
        private async Task<List<Product>> SearchProductsSecure(string keyword)
        {
            return await _db.Products
                .FromSqlInterpolated($@"SELECT * FROM Products WHERE Name LIKE '%' || {keyword} || '%'")
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
