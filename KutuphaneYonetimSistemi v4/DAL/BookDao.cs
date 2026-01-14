using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi_v4.Domain;
using MySql.Data.MySqlClient;

namespace KutuphaneYonetimSistemi_v4.DAL
{
    public class BookDao
    {
        private string connectionString = "Server=172.21.54.253;Database=26_132430071;Uid=26_132430071;Pwd=İnif123.;";

        // 1. TÜM KİTAPLARI LİSTELE
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Books";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(MapReaderToBook(reader));
                        }
                    }
                }
            }
            return books;
        }

        // 2. KİTAP EKLEME
        public void AddBook(Book book)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Books 
                                (Title, Author, ISBN, Publisher, PublishYear, Stock, PageCount, Category, SubCategory, IsAvailable) 
                                VALUES 
                                (@Title, @Author, @ISBN, @Publisher, @PublishYear, @Stock, @PageCount, @Category, @SubCategory, @IsAvailable)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    AddParameters(cmd, book);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 3. KİTAP SİLME
        public void DeleteBook(int bookId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Books WHERE BookId = @BookId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. GÜNCELLEME (UPDATE)
        public void Update(Book book)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Books SET 
                                Title=@Title, Author=@Author, ISBN=@ISBN, 
                                Publisher=@Publisher, PublishYear=@PublishYear, 
                                Stock=@Stock, PageCount=@PageCount, 
                                Category=@Category, SubCategory=@SubCategory 
                                WHERE BookId=@BookId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    AddParameters(cmd, book);
                    cmd.Parameters.AddWithValue("@BookId", book.BookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 5. ARAMA METODU (SEARCH)
        public List<Book> Search(string arananKelime)
        {
            List<Book> books = new List<Book>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE Title LIKE @Search OR Author LIKE @Search OR ISBN LIKE @Search";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + arananKelime + "%");
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(MapReaderToBook(reader));
                        }
                    }
                }
            }
            return books;
        }

        // YARDIMCI METOD: Reader'dan Book nesnesine dönüştürme (Kod tekrarını önler)
        private Book MapReaderToBook(MySqlDataReader reader)
        {
            return new Book
            {
                BookId = Convert.ToInt32(reader["BookId"]),
                Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "",
                Author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "",
                ISBN = reader["ISBN"] != DBNull.Value ? reader["ISBN"].ToString() : "",
                Publisher = reader["Publisher"] != DBNull.Value ? reader["Publisher"].ToString() : "",
                Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "-",
                SubCategory = reader["SubCategory"] != DBNull.Value ? reader["SubCategory"].ToString() : "-",
                PublishYear = reader["PublishYear"] != DBNull.Value ? Convert.ToInt32(reader["PublishYear"]) : 0,
                Stock = reader["Stock"] != DBNull.Value ? Convert.ToInt32(reader["Stock"]) : 0,
                PageCount = reader["PageCount"] != DBNull.Value ? Convert.ToInt32(reader["PageCount"]) : 0,
                IsAvailable = reader["IsAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsAvailable"])
            };
        }

        // YARDIMCI METOD: Parametreleri ekleme (Kod tekrarını önler)
        private void AddParameters(MySqlCommand cmd, Book book)
        {
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
            cmd.Parameters.AddWithValue("@PublishYear", book.PublishYear);
            cmd.Parameters.AddWithValue("@Stock", book.Stock);
            cmd.Parameters.AddWithValue("@PageCount", book.PageCount);
            cmd.Parameters.AddWithValue("@Category", book.Category);
            cmd.Parameters.AddWithValue("@SubCategory", book.SubCategory);
            cmd.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
        }
    }
}




