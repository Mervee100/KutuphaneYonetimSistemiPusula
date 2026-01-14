using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4.DAL
{
    public class BorrowDao
    {
        private string baglantiCumlesi = "Server=172.21.54.253;Database=26_132430071;Uid=26_132430071;Pwd=İnif123.;";

        // 1. Ekleme İşlemi (Ödünç Verme)
        public void Add(Borrow borrow)
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "INSERT INTO Borrows (BookId, MemberId, BorrowDate, DueDate) VALUES (@bId, @mId, @bDate, @dDate)";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@bId", borrow.BookId);
                komut.Parameters.AddWithValue("@mId", borrow.MemberId);
                komut.Parameters.AddWithValue("@bDate", borrow.BorrowDate);
                komut.Parameters.AddWithValue("@dDate", borrow.DueDate);
                komut.ExecuteNonQuery();
            }
        }

        // 2. Güncelleme İşlemi (İade Alma)
        public void UpdateReturnDate(int borrowId)
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "UPDATE Borrows SET ReturnDate = @rDate WHERE BorrowId = @id";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@rDate", DateTime.Now);
                komut.Parameters.AddWithValue("@id", borrowId);
                komut.ExecuteNonQuery();
            }
        }

        // 3. Kontrol İşlemi (Kitap şu an başkasında mı?)
        public bool IsBookCurrentlyBorrowed(int bookId)
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // Kitap verilmiş ama henüz iade tarihi (ReturnDate) girilmemiş mi?
                string sorgu = "SELECT COUNT(*) FROM Borrows WHERE BookId = @bId AND ReturnDate IS NULL";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@bId", bookId);

                int sonuc = Convert.ToInt32(komut.ExecuteScalar());
                return sonuc > 0; // 0'dan büyükse kitap dışarıdadır.
            }
        }

        // 4. Listeleme İşlemi (Tüm kayıtları çekme)
        public List<Borrow> GetAll()
        {
            List<Borrow> list = new List<Borrow>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM Borrows ORDER BY BorrowDate DESC";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Borrow b = new Borrow();
                        b.BorrowId = Convert.ToInt32(okuyucu["BorrowId"]);
                        b.BookId = Convert.ToInt32(okuyucu["BookId"]);
                        b.MemberId = Convert.ToInt32(okuyucu["MemberId"]);
                        b.BorrowDate = Convert.ToDateTime(okuyucu["BorrowDate"]);
                        b.DueDate = Convert.ToDateTime(okuyucu["DueDate"]);
                        if (okuyucu["ReturnDate"] != DBNull.Value)
                            b.ReturnDate = Convert.ToDateTime(okuyucu["ReturnDate"]);
                        else
                            b.ReturnDate = null;

                        list.Add(b);
                    }
                }
            }
            return list;
        }
        public System.Collections.Generic.List<BorrowDetail> GetBorrowDetails()
        {
            var liste = new System.Collections.Generic.List<BorrowDetail>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                
                string sorgu = @"
                SELECT 
                b.BorrowId, 
                k.Title AS KitapAdi, 
                CONCAT(u.FirstName, ' ', u.LastName) AS UyeAdi, 
                b.BorrowDate, 
                b.DueDate, 
                b.ReturnDate 
                FROM Borrows b
                JOIN Books k ON b.BookId = k.BookId 
                JOIN Members u ON b.MemberId = u.MemberId
                ORDER BY b.BorrowDate DESC";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        BorrowDetail detay = new BorrowDetail();
                        detay.BorrowId = Convert.ToInt32(okuyucu["BorrowId"]);
                        detay.KitapAdi = okuyucu["KitapAdi"].ToString();
                        detay.UyeAdi = okuyucu["UyeAdi"].ToString(); 
                        detay.BorrowDate = Convert.ToDateTime(okuyucu["BorrowDate"]);
                        detay.DueDate = Convert.ToDateTime(okuyucu["DueDate"]);

                        if (okuyucu["ReturnDate"] != DBNull.Value)
                            detay.ReturnDate = Convert.ToDateTime(okuyucu["ReturnDate"]);
                        else
                            detay.ReturnDate = null;

                        liste.Add(detay);
                    }
                }
            }
            return liste;
        }

    }
}

