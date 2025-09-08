using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders/Checkout
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: Orders/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save order to DB
                return RedirectToAction("Confirmation");
            }
            return View(order);
        }

        // GET: Orders/Confirmation
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
