using Microsoft.AspNetCore.Mvc;

namespace EcommerceRoutingDemo.Controllers
{
    public class ProductsController : Controller
    {
        // Action to show product details
        public IActionResult Details(string category, int id)
        {
            return Content($"Product: {category}, ID: {id}");
        }

        // Action to filter products
        public IActionResult Filter(string category, string priceRange)
        {
            return Content($"Filtering {category} with price range {priceRange}");
        }
    }
}
