using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using KutuphaneYonetimSistemi_v4.Service; 
using KutuphaneYonetimSistemi_v4.Domain;  

namespace KutuphaneYonetimSistemi_v4
{
    public partial class FormRapor : Form
    {
        // --- PENCERE SÜRÜKLEME KODLARI ---
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // --- SERVİSLER ---
        private ReportService _reportService;
        private BookService _bookService;
        private MemberService _memberService;

        // --- RENK PALETİ ---
        private readonly Color Turkuaz = Color.FromArgb(23, 162, 184);
        private readonly Color Kirmizi = Color.Crimson;
        private readonly Color Gri = Color.Gray;

        public FormRapor()
        {
            InitializeComponent();
            _reportService = new ReportService();
            _bookService = new BookService();
            _memberService = new MemberService();

            if (btnGosterKategori != null) btnGosterKategori.Click += new EventHandler(btnGosterKategori_Click);
            if (btnGosterDurum != null) btnGosterDurum.Click += new EventHandler(btnGosterDurum_Click);
        }

        private void FormRapor_Load(object sender, EventArgs e)
        {
            try
            {
                IstatistikleriDoldur();
                KartlariDoldur();
                GrafikleriCiz(); 

                if (btnGosterKategori != null)
                    btnGosterKategori.PerformClick();

                RakamlariRenklendir();
                btnBuAyEklenenler_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken hata oluştu: " + ex.Message);
            }
        }
        private void GrafikleriCiz()
        {
            try
            {
                // --- AYLIK İSTATİSTİK ---
                var aylikVeri = _reportService.GetAylikIstatistik();
                if (chartAylik.Series.Count > 0)
                {
                    chartAylik.Series[0].Points.Clear();
                    chartAylik.Series[0].Color = Turkuaz;

                    foreach (var veri in aylikVeri)
                    {
                        chartAylik.Series[0].Points.AddXY(veri.Ay, veri.IslemSayisi);
                    }
                }

                // --- YAŞ DAĞILIMI ---
                var yasVerileri = _reportService.GetUyeYasDagilimi();
                chartYas.Series.Clear();
                chartYas.Series.Add("Yaşlar");
                chartYas.Series["Yaşlar"].ChartType = SeriesChartType.Column;
                chartYas.Series["Yaşlar"].Color = Turkuaz;
                foreach (var veri in yasVerileri) chartYas.Series["Yaşlar"].Points.AddXY(veri.Key, veri.Value);

                // --- PASTA GRAFİĞİ ---
                GrafikCiz_Pie(chartKategori, _reportService.GetKategoriIstatistikleri());
            }
            catch (Exception ex)
            {

            }
        }

        // --- Butonla Grafik Değiştirme (Kategori) ---
        private void btnGosterKategori_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblGrafikBaslik != null) lblGrafikBaslik.Text = "Kitapların Kategori Dağılımı";
                AktifButonAyarla(btnGosterKategori, btnGosterDurum);
                GrafikCiz_Pie(chartKategori, _reportService.GetKategoriIstatistikleri());
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        // --- Butonla Grafik Değiştirme (Durum) ---
        private void btnGosterDurum_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblGrafikBaslik != null) lblGrafikBaslik.Text = "Kitapların Durum Dağılımı";
                AktifButonAyarla(btnGosterDurum, btnGosterKategori);
                var durumVerileri = _reportService.GetKitapDurumDagilimi();

                chartKategori.Series.Clear();
                chartKategori.Legends.Clear(); // Lejantı sil

                var seri = chartKategori.Series.Add("Durumlar");
                seri.ChartType = SeriesChartType.Doughnut;

                seri.Label = "#AXISLABEL (#VALY)";
                seri["PieLabelStyle"] = "Outside";
                seri.BorderWidth = 1;
                seri.BorderColor = Color.White;

                foreach (var item in durumVerileri)
                {
                    seri.Points.AddXY(item.Key, item.Value);
                }

                TurkuazTonlariUygula(seri);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // --- ORTAK PASTA GRAFİĞİ METODU (Temizlik ve Standart İçin) ---
        private void GrafikCiz_Pie(Chart chart, dynamic veriListesi)
        {
            chartKategori.Series.Clear();
            chartKategori.Legends.Clear();

            // 2. LEJANTI (Açıklama Kutusu) Aşağıya Ekle
            chartKategori.Legends.Add("GenelLejant");
            chartKategori.Legends["GenelLejant"].Docking = Docking.Bottom;
            chartKategori.Legends["GenelLejant"].Alignment = StringAlignment.Center;

            // 3. Seriyi Oluştur
            var seri = chartKategori.Series.Add("Veriler");
            seri.ChartType = SeriesChartType.Doughnut;  

            // Görsellik Ayarları
            seri["PieLabelStyle"] = "Outside"; 
            seri.BorderColor = Color.White;

            // --- VERİLERİ VE ETİKETLERİ AYARLAR ---
            foreach (var veri in veriListesi)
            {
                try
                {
                    string tamAd = veri.KategoriAdi; 
                    int sayi = veri.KitapSayisi;   
                    int pIndex = seri.Points.AddXY(tamAd, sayi);
                    DataPoint nokta = seri.Points[pIndex];
                    string kisaAd = tamAd.Split(' ')[0];
                    nokta.Label = kisaAd;       
                    nokta.LegendText = tamAd;  
                }
                catch { }
            }

            TurkuazTonlariUygula(seri);
        }

        // TOPLAM KİTAPLAR 
        private void btnListeKitap_Click(object sender, EventArgs e)
        {
            try
            {
                TabloyuGuncelle(_bookService.TumKitaplariGetir(), "Tüm Kitaplar Listesi");

                SutunBasligiAyarla("Title", "Kitap Adı");
                SutunBasligiAyarla("Author", "Yazar");
                SutunBasligiAyarla("Publisher", "Yayınevi");
                SutunBasligiAyarla("PageCount", "Sayfa Sayısı");
                SutunBasligiAyarla("Stock", "Stok");
                SutunBasligiAyarla("ISBN", "ISBN No");
                SutunBasligiAyarla("Category", "Kategori");

                // Gizlenecekler
                SutunGizle("BookId");
                SutunGizle("SubCategory");
                SutunGizle("IsAvailable");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        // 2. TOPLAM ÜYELER 
        private void btnListeUye_Click(object sender, EventArgs e)
        {
            try
            {
                TabloyuGuncelle(_memberService.GetAll(), "Tüm Üyeler Listesi");

                SutunBasligiAyarla("FirstName", "Ad");
                SutunBasligiAyarla("LastName", "Soyad");
                SutunBasligiAyarla("Email", "E-Posta");
                SutunBasligiAyarla("Phone", "Telefon");
                SutunBasligiAyarla("RegistrationDate", "Kayıt Tarihi");

                // Gizlenecekler
                SutunGizle("MemberId");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        // 3. EMANETTEKİ KİTAPLAR (BorrowID, BookID gizlenir)
        private void btnListeEmanet_Click(object sender, EventArgs e)
        {
            try
            {
                TabloyuGuncelle(_reportService.EmanetListesiniGetir(), "Şu An Emanette Olan Kitaplar");

                SutunBasligiAyarla("Title", "Kitap Adı");
                SutunBasligiAyarla("BorrowDate", "Veriliş Tarihi");
                SutunBasligiAyarla("DueDate", "Son Teslim");
                SutunBasligiAyarla("MemberName", "Üye Adı");
                SutunBasligiAyarla("FirstName", "Üye Adı"); 

                // Gizlenecekler
                SutunGizle("BorrowId");
                SutunGizle("BookId");
                SutunGizle("MemberId");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        // 4. SON EKLENEN KİTAPLAR
        private void btnBuAyEklenenler_Click(object sender, EventArgs e)
        {
            try
            {
                TabloyuGuncelle(_reportService.SonEklenenleriGetir(), "Kütüphaneye Son Eklenen Kitaplar");

                SutunBasligiAyarla("Title", "Kitap Adı");
                SutunBasligiAyarla("Author", "Yazar");
                SutunBasligiAyarla("Publisher", "Yayınevi");
                SutunBasligiAyarla("PageCount", "Sayfa");
                SutunBasligiAyarla("Stock", "Stok");
                SutunBasligiAyarla("ISBN", "ISBN No");
                SutunBasligiAyarla("Category", "Kategori");
                SutunBasligiAyarla("SubCategory", "Alt Kategori");

                // Gizlenecekler
                SutunGizle("BookId");
                SutunGizle("CreatedDate");
                SutunGizle("IsAvailable");
            }
            catch { }
        }

        // 5. GECİKEN KİTAPLAR
        private void btnGecikenler_Click(object sender, EventArgs e)
        {
            try
            {
                var liste = _reportService.GetGecikenKitaplar();
                TabloyuGuncelle(liste, "Geciken (İadesi Geçmiş) Kitaplar");

                SutunBasligiAyarla("BookTitle", "Kitap Adı");
                SutunBasligiAyarla("MemberName", "Üye Adı");
                SutunBasligiAyarla("DueDate", "Son Teslim");
                SutunGizle("BorrowId");

                if (liste != null && liste.Count == 0)
                    MessageBox.Show("Şu an geciken kitap bulunmamaktadır. Harika!");
            }
            catch { }
        }

        // 6. POPÜLER KİTAPLAR
        private void btnPopuler_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.GetPopulerKitaplar(), "En Popüler Kitaplar");
            SutunBasligiAyarla("KitapAdi", "Kitap Adı");
            SutunBasligiAyarla("OkunmaSayisi", "Okunma Sayısı");
            SutunGizle("BookId");
        }

        // 7. AKTİF ÜYELER
        private void btnAktifUyeler_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.GetAktifUyeler(), "En Çok Okuyan Üyeler");
            SutunBasligiAyarla("UyeAdi", "Üye Adı");
            SutunBasligiAyarla("OkuduguKitapSayisi", "Toplam Ödünç");
            SutunGizle("MemberId");
        }

        // 8. POPÜLER YAZARLAR
        private void btnPopülerYazar_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.EnSevilenYazarlariGetir(), "En Popüler Yazarlar (Top 10)");
            SutunBasligiAyarla("YazarAdi", "Yazar Adı");
            SutunBasligiAyarla("OkunmaSayisi", "Okunma Sayısı");
        }

        // 9. HAYALET (PASİF) ÜYELER
        private void btnHayaletUyeler_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.HayaletUyeGetir(), "Uzun Süredir Kitap Almayan Üyeler (6 Ay+)");
            SutunBasligiAyarla("FirstName", "Ad");
            SutunBasligiAyarla("LastName", "Soyad");
            SutunBasligiAyarla("Email", "E-Posta");
            SutunGizle("MemberId");
        }

        // 10. KULLANILMAYAN KİTAPLAR
        private void btnAtilKitaplar_Click(object sender, EventArgs e)
        {
            try
            {
                TabloyuGuncelle(_reportService.KullanilmayanKitaplariListele(), "1 Yıldır Hiç Alınmayan Kitaplar");
                SutunBasligiAyarla("Title", "Kitap Adı");
                SutunBasligiAyarla("Author", "Yazar");
                SutunBasligiAyarla("Publisher", "Yayınevi");
                SutunGizle("BookId");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void btnAtilKitaplar_Click_1(object sender, EventArgs e)
        {
            btnAtilKitaplar_Click(sender, e);
        }

        // 11. DİĞER BUTONLAR
        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.GetKategoriIstatistikleri(), "Kategori Listesi");
            SutunBasligiAyarla("KategoriAdi", "Kategori");
            SutunBasligiAyarla("KitapSayisi", "Kitap Adedi");
        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.GetAylikIstatistik(), "Aylık İşlem Raporu");
            SutunBasligiAyarla("Ay", "Dönem (Yıl-Ay)");
            SutunBasligiAyarla("IslemSayisi", "Verilen Kitap Sayısı");
        }

        private void btnOkumaSureleri_Click(object sender, EventArgs e)
        {
            TabloyuGuncelle(_reportService.OrtalamaSureleriGetir(), "Kitapların Ortalama Bitirilme Süresi (Gün)");
        }
        private void TabloyuGuncelle(object veri, string baslik)
        {
            dgvRaporListesi.DataSource = null;
            dgvRaporListesi.DataSource = veri;

           
            if (lblRaporBaslik != null) lblRaporBaslik.Text = baslik;

            dgvRaporListesi.ColumnHeadersHeight = 45;
            dgvRaporListesi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRaporListesi.RowHeadersVisible = false; 
        }

        private void SutunBasligiAyarla(string kolon, string baslik)
        {
            if (dgvRaporListesi.Columns[kolon] != null)
                dgvRaporListesi.Columns[kolon].HeaderText = baslik;
        }

        private void SutunGizle(string kolon)
        {
            if (dgvRaporListesi.Columns[kolon] != null)
                dgvRaporListesi.Columns[kolon].Visible = false;
        }

        private void AktifButonAyarla(Button aktif, Button pasif)
        {
            aktif.BackColor = Turkuaz;
            aktif.ForeColor = Color.White;
            pasif.BackColor = Color.LightGray;
            pasif.ForeColor = Color.Black;
        }

        private void TurkuazTonlariUygula(Series seri)
        {
            var points = seri.Points;
            for (int i = 0; i < points.Count; i++)
            {
                int r = Math.Min(255, Turkuaz.R + (i * 30));
                int g = Math.Min(255, Turkuaz.G + (i * 10));
                int b = Math.Min(255, Turkuaz.B + (i * 20));
                points[i].Color = Color.FromArgb(r, g, b);
                points[i].LabelForeColor = Color.Black;
            }
        }

        private void IstatistikleriDoldur()
        {
            try
            {
                int toplamUye = _reportService.GetTotalMemberCount();
                int aktifUye = _reportService.AktifUyeSayisiniGetir();
                double aktifYuzde = (toplamUye > 0) ? ((double)aktifUye / toplamUye) * 100 : 0;
                lblAktifUyeYuzde.Text = "%" + aktifYuzde.ToString("0.0");

                int toplamKitap = _reportService.GetTotalBookCount();
                int atilKitap = _reportService.KullanilmayanKitapSayisiniGetir();
                double atilYuzde = (toplamKitap > 0) ? ((double)atilKitap / toplamKitap) * 100 : 0;
                lblAtilKitapSayisi.Text = atilKitap.ToString();
                lblAtilKitapYuzde.Text = "%" + atilYuzde.ToString("0.0");
            }
            catch { }
        }

        private void KartlariDoldur()
        {
            lblToplamKitapSayisi.Text = _reportService.ToplamKitapSayisi().ToString();
            lblAktifUyeSayisi.Text = _reportService.ToplamUyeSayisi().ToString();
            lblEmanetSayisi.Text = _reportService.EmanetSayisi().ToString();
        }

        private void RakamlariRenklendir()
        {
            int net = _reportService.GetBuHaftakiUyeSayisi() - _reportService.GetBuHaftaSilinenUyeSayisi();
            if (net > 0) { lblUyeArtis.Text = $"(+{net} bu hafta)"; lblUyeArtis.ForeColor = Turkuaz; }
            else if (net < 0) { lblUyeArtis.Text = $"({net} bu hafta)"; lblUyeArtis.ForeColor = Kirmizi; }
            else { lblUyeArtis.Text = "(0 bu hafta)"; lblUyeArtis.ForeColor = Gri; }

            int gecikmis = _reportService.GetGecikmisKitapSayisi();
            if (lblGecikmisKitap != null)
            {
                lblGecikmisKitap.Text = gecikmis > 0 ? $"({gecikmis} gecikmiş)" : "(Gecikme Yok)";
                lblGecikmisKitap.ForeColor = gecikmis > 0 ? Kirmizi : Turkuaz;
            }

            int yeni = _reportService.GetBuAyEklenenKitapSayisi();
            if (lblYeniKitaplar != null)
            {
                lblYeniKitaplar.Text = yeni > 0 ? $"(+{yeni} yeni)" : "(Yeni kitap yok)";
                lblYeniKitaplar.ForeColor = yeni > 0 ? Turkuaz : Gri;
            }
        }

        private void dgvRaporListesi_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void btnRaporCikis_Click(object sender, EventArgs e) { this.Close(); }
    }
}