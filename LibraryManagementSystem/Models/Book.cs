namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }   // 👈 this is missing in your model, add this line

        public string Title { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;  // fixed warning with null-forgiving

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;   // fixed warning with null-forgiving
    }
}
