using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Details(string category, int id) => View();
        public IActionResult Filter(string category, string priceRange) => View();
    }
}

public class OrdersController : Controller
{
    public IActionResult UserOrders(string username) => View();
}
public class DashboardController : Controller
{
    public IActionResult Index(string role) => View();
}
public class DocsController : Controller
{
    public IActionResult View(Guid docId) => View();
}
