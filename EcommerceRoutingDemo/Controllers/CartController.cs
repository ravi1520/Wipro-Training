
using Microsoft.AspNetCore.Mvc;
public class CartController : Controller
{


    public IActionResult Checkout()
    {
        bool isLoggedIn = false; // Example condition
        if (!isLoggedIn)
            return RedirectToAction("Login", "Account");

        return Content("Proceeding to checkout...");
    }
}