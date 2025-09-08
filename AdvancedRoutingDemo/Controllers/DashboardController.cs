using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index(string role) => View();
    }
}
