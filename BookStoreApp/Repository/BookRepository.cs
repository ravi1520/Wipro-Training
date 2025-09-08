using System.Data;
using Microsoft.Data.SqlClient;
using BookstoreApp.Models;

namespace BookstoreApp.Repository
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all books
        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, BookName, Writer, Cost, Stock FROM Books", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            BookId = Convert.ToInt32(reader["Id"]),
                            Title = reader["BookName"].ToString()!,
                            Author = reader["Writer"].ToString()!,
                            Price = Convert.ToDecimal(reader["Cost"]),
                            Quantity = Convert.ToInt32(reader["Stock"])
                        });
                    }
                }
            }
            return books;
        }

        // Add book using Stored Procedure
        public void AddBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookName", book.Title);
                cmd.Parameters.AddWithValue("@Writer", book.Author);
                cmd.Parameters.AddWithValue("@Cost", book.Price);
                cmd.Parameters.AddWithValue("@Stock", book.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        // Update book
        public void UpdateBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UpdateBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", book.BookId);
                cmd.Parameters.AddWithValue("@BookName", book.Title);
                cmd.Parameters.AddWithValue("@Writer", book.Author);
                cmd.Parameters.AddWithValue("@Cost", book.Price);
                cmd.Parameters.AddWithValue("@Stock", book.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete book
        public void DeleteBook(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteBook", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
