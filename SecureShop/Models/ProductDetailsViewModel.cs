using SecureShop.Models;
using System.Collections.Generic;

namespace SecureShop.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; } = null!;
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
