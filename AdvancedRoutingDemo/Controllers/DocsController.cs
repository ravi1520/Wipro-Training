using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class DocsController : Controller
    {
        public IActionResult View(Guid docId) => View();
    }
}
