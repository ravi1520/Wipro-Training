using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
//using Newtonsoft.Json;
using System.Text.Json;


namespace OnlineBookStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int id, string title, decimal price)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item == null)
                cart.Add(new CartItem { BookId = id, Title = title, Quantity = 1, Price = price });
            else
                item.Quantity++;

            SaveCart(cart);
            return RedirectToAction("Index");
        }

       private List<CartItem> GetCart()
{
    var cart = HttpContext.Session.GetString("Cart");
    return cart == null 
        ? new List<CartItem>() 
        : JsonSerializer.Deserialize<List<CartItem>>(cart);
}

private void SaveCart(List<CartItem> cart)
{
    HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
}

    }
}
