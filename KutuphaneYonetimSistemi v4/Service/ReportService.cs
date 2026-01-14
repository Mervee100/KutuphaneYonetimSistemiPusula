using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi_v4.DAL;
using KutuphaneYonetimSistemi_v4.Domain;
using MySql.Data.MySqlClient;

namespace KutuphaneYonetimSistemi_v4.Service
{
    public class ReportService
    {
        private ReportDao _reportDao;

        public ReportService()
        {
            _reportDao = new ReportDao();
        }
        public int ToplamKitapSayisi()
        {
            return _reportDao.GetTotalBookCount();
        }

        public int ToplamUyeSayisi()
        {
            return _reportDao.GetTotalMemberCount();
        }

        public int EmanetSayisi()
        {
            return _reportDao.GetActiveBorrowCount();
        }
        public List<BorrowDetail> GetGecikenKitaplar()
        {
            return _reportDao.GetOverdueBooks();
        }
        public List<BookStat> GetPopulerKitaplar()
        {
            return _reportDao.GetMostPopularBooks();
        }
        public List<MemberStat> GetAktifUyeler()
        {
            return _reportDao.GetMostActiveMembers();
        }

        public List<CategoryStat> GetKategoriIstatistikleri()
        {
            return _reportDao.GetBooksByCategory();
        }

        public List<MonthlyStat> GetAylikIstatistik()
        {
            return _reportDao.GetMonthlyStats();
        }
        // Yazar İstatistiği
        public List<AuthorStat> EnSevilenYazarlariGetir()
        {
            return _reportDao.GetMostLovedAuthors();
        }

        // Ortalama Okuma Süresi (Doğrudan DataTable döndürelim, listeyle uğraşma)
        public DataTable OrtalamaSureleriGetir()
        {
            return _reportDao.GetAverageReadingTimes();
        }

        // Hayalet Üyeler
        public DataTable HayaletUyeGetir()
        {
            return _reportDao.GetGhostMembers();
        }
        public DataTable SonEklenenleriGetir()
        {
            return _reportDao.GetNewestBooks();
        }
        public int GetBuHaftakiUyeSayisi()
        {
            // Artık DAO'da bu metod var, hata vermeyecek.
            return _reportDao.GetHaftalikUyeSayisi(0);
        }

        // 2. Geçen Hafta Kayıt Olanlar (Fark = 1)
        public int GetGecenHaftakiUyeSayisi()
        {
            return _reportDao.GetHaftalikUyeSayisi(1);
        }
        public int GetBuHaftaSilinenUyeSayisi()
        {
            // _reportDao senin DAO nesnenin adı, onu kendine göre düzeltebilirsin
            return _reportDao.GetBuHaftaSilinenUyeSayisi();
        }
        public int GetGecikmisKitapSayisi()
        {
            return _reportDao.GetGecikmisKitapSayisi();
        }

        public int GetBuAyEklenenKitapSayisi()
        {
            return _reportDao.GetBuAyEklenenKitapSayisi();
        }
        public Dictionary<string, int> GetUyeYasDagilimi()
        {
            return _reportDao.GetUyeYasDagilimi();
        }

        public Dictionary<string, int> GetKitapDurumDagilimi()
        {
            return _reportDao.GetKitapDurumDagilimi();
        }
        public int AktifUyeSayisiniGetir()
        {
            return _reportDao.GetAktifUyeSayisi();
        }

        public int KullanilmayanKitapSayisiniGetir()
        {
            return _reportDao.GetKullanilmayanKitapSayisi();
        }

        public DataTable KullanilmayanKitaplariListele()
        {
            return _reportDao.GetKullanilmayanKitaplarListesi();
        }
        public int GetTotalBookCount()
        {
            return _reportDao.GetTotalBookCount();
        }

        // 2. Toplam Üye Sayısını Getir
        public int GetTotalMemberCount()
        {
            return _reportDao.GetTotalMemberCount();
        }
        public DataTable EmanetListesiniGetir()
        {
            return _reportDao.GetEmanettekiKitaplarDetayli();
        }
    }
}
