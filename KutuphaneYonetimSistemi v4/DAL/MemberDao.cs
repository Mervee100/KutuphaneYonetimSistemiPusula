using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4.DAL
{
    public class MemberDao
    {
        private string baglantiCumlesi = "Server=172.21.54.253;Database=26_132430071;Uid=26_132430071;Pwd=İnif123.;";

        // 1. EKLEME (INSERT)
        public void Add(Member uye)
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                string sorgu = "INSERT INTO Members (FirstName, LastName, Phone, Email, RegisterDate, BirthDate, Status) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @dogum, 'Aktif')";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", uye.Ad);
                komut.Parameters.AddWithValue("@p2", uye.Soyad);
                komut.Parameters.AddWithValue("@p3", uye.Telefon);
                komut.Parameters.AddWithValue("@p4", uye.Eposta);
                komut.Parameters.AddWithValue("@p5", DateTime.Now); // Kayıt tarihi


                if (uye.DogumTarihi.HasValue)
                {
                    komut.Parameters.AddWithValue("@dogum", uye.DogumTarihi.Value);
                }
                else
                {
                    komut.Parameters.AddWithValue("@dogum", DBNull.Value);
                }

                komut.ExecuteNonQuery();
            }
        }

        // 2. LİSTELEME (SELECT)
        public List<Member> GetAll()
        {
            List<Member> uyeler = new List<Member>();

            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM Members WHERE Status = 'Aktif'";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Member uye = new Member();
                        // DİKKAT: Parantez içleri veritabanındaki sütun adların!
                        uye.Id = Convert.ToInt32(okuyucu["MemberId"]);
                        uye.Ad = okuyucu["FirstName"].ToString();
                        uye.Soyad = okuyucu["LastName"].ToString();
                        uye.Telefon = okuyucu["Phone"].ToString();
                        uye.Eposta = okuyucu["Email"].ToString();
                        uye.UyelikTarihi = Convert.ToDateTime(okuyucu["RegisterDate"]);

                        if (okuyucu["BirthDate"] != DBNull.Value)
                        {
                            uye.DogumTarihi = Convert.ToDateTime(okuyucu["BirthDate"]);
                        }

                        uyeler.Add(uye);
                    }
                }
            }
            return uyeler;
        }

        // 3. SİLME (DELETE)
        public void Delete(int id)
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "UPDATE Members SET Status = 'Pasif', UpdateDate = NOW() WHERE MemberId = @p1";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", id);
                komut.ExecuteNonQuery();
            }
        }

        // 4. GÜNCELLEME (UPDATE)
        public void Update(Member uye)
        {
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "UPDATE Members SET FirstName=@p1, LastName=@p2, Phone=@p3, Email=@p4 WHERE MemberId=@p5";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", uye.Ad);
                komut.Parameters.AddWithValue("@p2", uye.Soyad);
                komut.Parameters.AddWithValue("@p3", uye.Telefon);
                komut.Parameters.AddWithValue("@p4", uye.Eposta);
                komut.Parameters.AddWithValue("@p5", uye.Id);

                komut.ExecuteNonQuery();
            }
        }
        // 5. ARAMA (SEARCH) - DÜZELTİLMİŞ HALİ
        public List<Member> Search(string aranan)
        {
            List<Member> uyeler = new List<Member>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                // DİKKAT: Hem 'Aktif' olmalı HEM DE (Adı VEYA Soyadı VEYA Telefonu uymalı)
                // Parantez kullanmazsan mantık hatası olur, pasifler de gelir.
                string sorgu = "SELECT * FROM Members WHERE Status = 'Aktif' AND (FirstName LIKE @p1 OR LastName LIKE @p1 OR Phone LIKE @p1)";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", "%" + aranan + "%");

                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Member uye = new Member();
                        uye.Id = Convert.ToInt32(okuyucu["MemberId"]);
                        uye.Ad = okuyucu["FirstName"].ToString();
                        uye.Soyad = okuyucu["LastName"].ToString();
                        uye.Telefon = okuyucu["Phone"].ToString();
                        uye.Eposta = okuyucu["Email"].ToString();
                        uye.UyelikTarihi = Convert.ToDateTime(okuyucu["RegisterDate"]);
                        uyeler.Add(uye);
                    }
                }
            }
            return uyeler;
        }

    }
}


