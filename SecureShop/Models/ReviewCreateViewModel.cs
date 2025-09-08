using System.ComponentModel.DataAnnotations;

namespace SecureShop.ViewModels
{
    public class ReviewCreateViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Content { get; set; } = string.Empty;
    }
}
