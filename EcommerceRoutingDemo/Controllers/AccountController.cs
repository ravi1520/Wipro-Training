
using Microsoft.AspNetCore.Mvc;
public class AccountController : Controller
{

    public IActionResult Login()
    {
        return Content("Please login to continue.");
    }
}
