namespace LibraryManagementSystem.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // fixes warning

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
