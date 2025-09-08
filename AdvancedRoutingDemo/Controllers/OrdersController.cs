using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult UserOrders(string username) => View();
    }
    [Route("orders")]
public class OrdersController : Controller
{
        [HttpGet("user")]
        public IActionResult UserOrders()
        {
            return View();
     }

        [HttpGet("user/all")]
        public IActionResult AllUserOrders()
        { 
            return View();
     }
}

}
