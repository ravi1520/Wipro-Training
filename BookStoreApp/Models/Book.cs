namespace BookstoreApp.Models
{
    public class Book
    {
        public int BookId { get; set; }          // maps to Id in DB
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
