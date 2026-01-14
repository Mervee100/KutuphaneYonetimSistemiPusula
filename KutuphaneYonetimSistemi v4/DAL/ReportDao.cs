using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using KutuphaneYonetimSistemi_v4.Domain;
using System.Data;

namespace KutuphaneYonetimSistemi_v4.DAL
{
    public class ReportDao
    {
        private string baglantiCumlesi = "Server=172.21.54.253;Database=26_132430071;Uid=26_132430071;Pwd=İnif123.;";

        //Geciken Kitapları Getir
        public List<BorrowDetail> GetOverdueBooks()
        {
            List<BorrowDetail> liste = new List<BorrowDetail>();
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
                    WHERE b.DueDate < CURDATE() AND b.ReturnDate IS NULL
                    ORDER BY b.DueDate ASC"; // En çok geciken en üstte görünsün

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

                        detay.ReturnDate = null;

                        liste.Add(detay);
                    }
                }
            }
            return liste;
        }
        public List<BookStat> GetMostPopularBooks()
        {
            List<BookStat> liste = new List<BookStat>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // SQL MANTIĞI:
                // 1. Kitapları ismine göre grupla.
                // 2. Her grupta kaç kayıt olduğunu say (COUNT).
                // 3. En çok okunandan aza doğru sırala (DESC).
                string sorgu = @"
            SELECT k.Title AS KitapAdi, COUNT(b.BookId) AS Sayi 
            FROM Borrows b
            JOIN Books k ON b.BookId = k.BookId
            GROUP BY k.Title
            ORDER BY Sayi DESC";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        BookStat istatistik = new BookStat();
                        istatistik.KitapAdi = okuyucu["KitapAdi"].ToString();
                        istatistik.OkunmaSayisi = Convert.ToInt32(okuyucu["Sayi"]);
                        liste.Add(istatistik);
                    }
                }
            }
            return liste;
        }
        //En Aktif Üyeler (Kim kaç kitap almış?)
        public List<MemberStat> GetMostActiveMembers()
        {
            List<MemberStat> liste = new List<MemberStat>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = @"
            SELECT 
                CONCAT(m.FirstName, ' ', m.LastName) AS AdSoyad, 
                COUNT(b.BorrowId) AS Sayi
            FROM Borrows b
            JOIN Members m ON b.MemberId = m.MemberId
            GROUP BY m.MemberId
            ORDER BY Sayi DESC";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        MemberStat stat = new MemberStat();
                        stat.UyeAdi = okuyucu["AdSoyad"].ToString();
                        stat.OkuduguKitapSayisi = Convert.ToInt32(okuyucu["Sayi"]);
                        liste.Add(stat);
                    }
                }
            }
            return liste;
        }

        //Kategori İstatistikleri (Hangi türden kaç kitap var?)
        public List<CategoryStat> GetBooksByCategory()
        {
            List<CategoryStat> liste = new List<CategoryStat>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // Books tablosundaki 'Category' sütununa göre grupluyoruz
                string sorgu = "SELECT Category, COUNT(*) AS Sayi FROM Books GROUP BY Category ORDER BY Sayi DESC";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        CategoryStat stat = new CategoryStat();
                        // Veritabanında kategori boşsa 'Belirsiz' yazalım
                        if (okuyucu["Category"] != DBNull.Value)
                            stat.KategoriAdi = okuyucu["Category"].ToString();
                        else
                            stat.KategoriAdi = "Kategorisiz";

                        stat.KitapSayisi = Convert.ToInt32(okuyucu["Sayi"]);
                        liste.Add(stat);
                    }
                }
            }
            return liste;
        }
        public List<MonthlyStat> GetMonthlyStats()
        {
            List<MonthlyStat> liste = new List<MonthlyStat>();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                // ADIM 1: Tarihi YIL-AY formatına çeviriyoruz (Örn: 2024-01).
                // ADIM 2: 'ASC' diyerek eskiden yeniye sıralıyoruz ki grafik düzgün dursun.
                string sorgu = @"
            SELECT DATE_FORMAT(BorrowDate, '%Y-%m') AS Donem, COUNT(*) AS Sayi 
            FROM Borrows 
            GROUP BY Donem 
            ORDER BY Donem ASC"; // DESC yerine ASC yaptık

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        MonthlyStat istatistik = new MonthlyStat();
                        istatistik.Ay = okuyucu["Donem"].ToString();
                        istatistik.IslemSayisi = Convert.ToInt32(okuyucu["Sayi"]);
                        liste.Add(istatistik);
                    }
                }
            }
            return liste;
        }
        // Toplam Kitap Sayısı
        public int GetTotalBookCount()
        {
            int count = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "SELECT COUNT(*) FROM Books";
                using (MySqlCommand komut = new MySqlCommand(sorgu, baglanti))
                {
                    count = Convert.ToInt32(komut.ExecuteScalar());
                }
            }
            return count;
        }

        // Aktif Üye Sayısı
        public int GetTotalMemberCount()
        {
            int count = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "SELECT COUNT(*) FROM Members";
                using (MySqlCommand komut = new MySqlCommand(sorgu, baglanti))
                {
                    count = Convert.ToInt32(komut.ExecuteScalar());
                }
            }
            return count;
        }

        //  Emanetteki (İade edilmemiş) Kitap Sayısı
        public int GetActiveBorrowCount()
        {
            int count = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = "SELECT COUNT(*) FROM Borrows WHERE ReturnDate IS NULL";
                using (MySqlCommand komut = new MySqlCommand(sorgu, baglanti))
                {
                    count = Convert.ToInt32(komut.ExecuteScalar());
                }
            }
            return count;
        }

        //EN SEVİLEN YAZARLAR (Hangi yazarın kitapları daha çok ödünç alınmış?)
        public List<AuthorStat> GetMostLovedAuthors()
        {
            List<AuthorStat> list = new List<AuthorStat>();
            using (MySqlConnection conn = new MySqlConnection(baglantiCumlesi))
            {
                conn.Open();
                string query = @"
            SELECT k.Author, COUNT(b.BorrowId) as OkunmaSayisi 
            FROM Books k 
            JOIN Borrows b ON k.BookId = b.BookId 
            GROUP BY k.Author 
            ORDER BY OkunmaSayisi DESC LIMIT 10"; // İlk 10 yazar

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AuthorStat
                            {
                                YazarAdi = reader["Author"].ToString(),
                                OkunmaSayisi = Convert.ToInt32(reader["OkunmaSayisi"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        //ORTALAMA OKUMA SÜRELERİ (Kitaplar ortalama kaç günde bitip iade ediliyor?)
        public DataTable GetAverageReadingTimes()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(baglantiCumlesi))
            {
                conn.Open();
                // DATEDIFF(ReturnDate, BorrowDate) iki tarih arasındaki gün farkını verir.
                string query = @"
            SELECT k.Title as 'Kitap Adı', 
                   ROUND(AVG(DATEDIFF(b.ReturnDate, b.BorrowDate)), 1) as 'Ort. Gün'
            FROM Borrows b
            JOIN Books k ON b.BookId = k.BookId
            WHERE b.ReturnDate IS NOT NULL
            GROUP BY k.BookId
            ORDER BY AVG(DATEDIFF(b.ReturnDate, b.BorrowDate)) ASC"; // En hızlı okunanlar üstte

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //HAYALET ÜYELER (Son 6 aydır hiç kitap almayan veya hiç işlem yapmamış üyeler)
        public DataTable GetGhostMembers()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(baglantiCumlesi))
            {
                conn.Open();
                // Mantık: Son işlem tarihi 6 aydan eski olanlar
                string query = @"
            SELECT m.FirstName, m.LastName, m.Email, MAX(b.BorrowDate) as 'Son İşlem'
            FROM Members m
            LEFT JOIN Borrows b ON m.MemberId = b.MemberId
            GROUP BY m.MemberId
            HAVING MAX(b.BorrowDate) < DATE_SUB(NOW(), INTERVAL 6 MONTH) OR MAX(b.BorrowDate) IS NULL";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
        // SON EKLENEN KİTAPLAR (En yeni kayıtlar)
        public DataTable GetNewestBooks()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(baglantiCumlesi))
            {
                conn.Open();
                string query = "SELECT * FROM Books ORDER BY BookId DESC LIMIT 20";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public int GetHaftalikUyeSayisi(int haftaFarki)
        {
            int sayi = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                string sorgu = @"SELECT COUNT(*) FROM Members 
                         WHERE YEAR(RegisterDate) = YEAR(CURDATE()) 
                         AND WEEK(RegisterDate, 1) = WEEK(CURDATE(), 1) - @Fark";

                using (MySqlCommand komut = new MySqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@Fark", haftaFarki);
                    // 0 ise bu hafta, 1 ise geçen hafta

                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                    {
                        sayi = Convert.ToInt32(sonuc);
                    }
                }
            }
            return sayi;
        }
        public int GetBuHaftaSilinenUyeSayisi()
        {
            int sayi = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                string sorgu = @"SELECT COUNT(*) FROM Members 
                         WHERE Status = 'Pasif' 
                         AND YEAR(UpdateDate) = YEAR(CURDATE()) 
                         AND WEEK(UpdateDate, 1) = WEEK(CURDATE(), 1)";

                using (MySqlCommand komut = new MySqlCommand(sorgu, baglanti))
                {
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                    {
                        sayi = Convert.ToInt32(sonuc);
                    }
                }
            }
            return sayi;
        }
        // GECİKMİŞ KİTAPLARI SAYAN METOT
        public int GetGecikmisKitapSayisi()
        {
            int sayi = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // İadesi boş olan (gelmemiş) VE Teslim Tarihi bugünden küçük (geçmiş) olanlar
                string sql = "SELECT COUNT(*) FROM Borrows WHERE ReturnDate IS NULL AND DueDate < CURDATE()";

                using (MySqlCommand komut = new MySqlCommand(sql, baglanti))
                {
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                    {
                        sayi = Convert.ToInt32(sonuc);
                    }
                }
            }
            return sayi;
        }
        // BU AY EKLENEN KİTAP SAYISI
        public int GetBuAyEklenenKitapSayisi()
        {
            int sayi = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // Sadece BU YIL ve BU AY eklenenleri say
                string sql = "SELECT COUNT(*) FROM Books WHERE YEAR(CreatedDate) = YEAR(CURDATE()) AND MONTH(CreatedDate) = MONTH(CURDATE())";

                using (MySqlCommand komut = new MySqlCommand(sql, baglanti))
                {
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                    {
                        sayi = Convert.ToInt32(sonuc);
                    }
                }
            }
            return sayi;
        }
        // YAŞ DAĞILIMI VERİSİ
        public Dictionary<string, int> GetUyeYasDagilimi()
        {
            Dictionary<string, int> veriListesi = new Dictionary<string, int>();

            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                // SQL'DE CASE WHEN İLE GRUPLAMA YAPIYORUZ
                string sql = @"
                    SELECT 
                        CASE 
                            WHEN (YEAR(CURDATE()) - YEAR(BirthDate)) BETWEEN 0 AND 14 THEN '0-14 Çocuk'
                            WHEN (YEAR(CURDATE()) - YEAR(BirthDate)) BETWEEN 15 AND 24 THEN '15-24 Genç'
                            WHEN (YEAR(CURDATE()) - YEAR(BirthDate)) BETWEEN 25 AND 64 THEN '25-64 Yetişkin'
                            ELSE '65+ Yaşlı'
                        END AS YasGrubu,
                        COUNT(*) AS Sayi
                    FROM Members
                    WHERE BirthDate IS NOT NULL
                    GROUP BY YasGrubu
                    ORDER BY YasGrubu ASC";

                using (MySqlCommand komut = new MySqlCommand(sql, baglanti))
                {
                    using (MySqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string grupAdi = okuyucu["YasGrubu"].ToString();
                            int sayi = Convert.ToInt32(okuyucu["Sayi"]);

                            if (!veriListesi.ContainsKey(grupAdi))
                            {
                                veriListesi.Add(grupAdi, sayi);
                            }
                        }
                    }
                }
            }
            return veriListesi;
        }
        
        public Dictionary<string, int> GetKitapDurumDagilimi()
        {
            Dictionary<string, int> veriListesi = new Dictionary<string, int>();

            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                // ADIM 1: Önce şu an dışarıda olan (geri gelmemiş) kitapları sayıyoruz.
                // Borrows tablosunda 'ReturnDate' BOŞ ise kitap dışarıdadır.
                string sqlOduncte = "SELECT COUNT(*) FROM Borrows WHERE ReturnDate IS NULL";
                int odunctekiSayi = 0;

                using (MySqlCommand komut = new MySqlCommand(sqlOduncte, baglanti))
                {
                    odunctekiSayi = Convert.ToInt32(komut.ExecuteScalar());
                }

                // ADIM 2: Toplam kitap sayısını buluyoruz.
                string sqlToplam = "SELECT COUNT(*) FROM Books";
                int toplamSayi = 0;

                using (MySqlCommand komut = new MySqlCommand(sqlToplam, baglanti))
                {
                    toplamSayi = Convert.ToInt32(komut.ExecuteScalar());
                }

                // ADIM 3: Raftakileri hesaplıyoruz (Toplam - Dışarıdakiler)
                int raftakiSayi = toplamSayi - odunctekiSayi;

                // ADIM 4: Listeye ekliyoruz. Artık elimizde kesin veriler var.
                veriListesi.Add("Rafta", raftakiSayi);
                veriListesi.Add("Ödünçte (Emanet)", odunctekiSayi);
            }

            return veriListesi;
        }

        //AKTİF ÜYE SAYISI (Son 6 ayda en az 1 kitap almış üyeler)
        public int GetAktifUyeSayisi()
        {
            int sayi = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // BorrowDate'i bugünden geriye 6 ay içinde olan üyeleri (tekil) say
                string sql = @"SELECT COUNT(DISTINCT MemberId) FROM Borrows 
                               WHERE BorrowDate >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)";

                using (MySqlCommand komut = new MySqlCommand(sql, baglanti))
                {
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                        sayi = Convert.ToInt32(sonuc);
                }
            }
            return sayi;
        }

        // 2. KULLANILMAYAN KİTAPLAR (+1 YIL veya HİÇ ALINMAMIŞ)
        public int GetKullanilmayanKitapSayisi()
        {
            int sayi = 0;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                // MANTIK: Tüm kitaplar ARASINDAN, son 1 yılda ödünç alınanları ÇIKAR.
                string sql = @"SELECT COUNT(*) FROM Books 
                               WHERE BookId NOT IN (
                                   SELECT DISTINCT BookId FROM Borrows 
                                   WHERE BorrowDate >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR)
                               )";

                using (MySqlCommand komut = new MySqlCommand(sql, baglanti))
                {
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null && sonuc != DBNull.Value)
                        sayi = Convert.ToInt32(sonuc);
                }
            }
            return sayi;
        }

        // 3. KULLANILMAYAN KİTAPLARIN LİSTESİ (Tıklayınca Tabloya Gelecek Olan)
        public DataTable GetKullanilmayanKitaplarListesi()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sql = @"SELECT * FROM Books 
                               WHERE BookId NOT IN (
                                   SELECT DISTINCT BookId FROM Borrows 
                                   WHERE BorrowDate >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR)
                               )";
                using (MySqlDataAdapter da = new MySqlDataAdapter(sql, baglanti))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetEmanettekiKitaplarDetayli()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();
                string sorgu = @"
                    SELECT 
                        b.BorrowId,
                        k.Title AS 'Kitap Adı',
                        CONCAT(m.FirstName, ' ', m.LastName) AS 'Üye Adı',
                        b.BorrowDate AS 'Veriliş Tarihi',
                        b.DueDate AS 'Son Teslim Tarihi'
                    FROM Borrows b
                    JOIN Books k ON b.BookId = k.BookId
                    JOIN Members m ON b.MemberId = m.MemberId
                    WHERE b.ReturnDate IS NULL
                    ORDER BY b.BorrowDate DESC";

                using (MySqlDataAdapter da = new MySqlDataAdapter(sorgu, baglanti))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

    }
}
