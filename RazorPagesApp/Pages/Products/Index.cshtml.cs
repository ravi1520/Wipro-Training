using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using System.Collections.Generic;

namespace RazorPagesApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        public List<Product> Products { get; set; } = new List<Product>();

        public void OnGet()
        {
            // Example Data
            Products.Add(new Product {
                ProductID = 1,
                Name = "Laptop",
                Description = "High performance laptop",
                Categories = new List<Category> {
                    new Category { CategoryID = 1, CategoryName = "Electronics" }
                }
            });
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Products.Add(NewProduct);
            }
            return Page();
        }
    }
}
