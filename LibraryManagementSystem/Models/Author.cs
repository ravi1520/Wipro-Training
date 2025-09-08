namespace LibraryManagementSystem.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // fixes warning

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
