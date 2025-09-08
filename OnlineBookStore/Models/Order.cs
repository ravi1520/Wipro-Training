using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = string.Empty;

        // Optional: Link order with a Book
        public int? BookId { get; set; }
        public Book? Book { get; set; }
    }
}
