using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "Invalid ISBN format. Use 123-1234567890")]
        public string ISBN { get; set; }

       [Column(TypeName = "decimal(18,2)")]
public decimal Price { get; set; }
    }
}
