using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KutuphaneYonetimSistemi_v4.Domain;
using KutuphaneYonetimSistemi_v4.Service;

namespace KutuphaneYonetimSistemi_v4
{
    public partial class FormKitap : Form
    {
        private BookService _bookService;
        private int _secilenKitapId = 0;
        private int _gelenRolId;

        Dictionary<string, List<string>> kategoriler = new Dictionary<string, List<string>>();

        public FormKitap(int roleId)
        {
            InitializeComponent();
            _bookService = new BookService();
            _gelenRolId = roleId;
        }

        //  FORM YÜKLENİRKEN 
        private void FormKitap_Load(object sender, EventArgs e)
        {
            // 1. Kategorileri Hafızaya Yükle
            KategoriVerileriniHazirla();

            // 2. Ana Kategori Kutusunu Doldur
            cmbAnaKategori.DataSource = new BindingSource(kategoriler, null);
            cmbAnaKategori.DisplayMember = "Key";
            cmbAnaKategori.ValueMember = "Key";

            // Tetiklenmeyi önlemek ve temiz başlatmak için:
            cmbAnaKategori.SelectedIndex = -1;
            cmbAltKategori.DataSource = null; 
            cmbAltKategori.Enabled = false;   

            Listele();

            // 4. Yetki Kontrolü (Rol ID 3 ise kısıtla)
            if (_gelenRolId == 3)
            {
                btnKaydet.Visible = false;
                btnSil.Visible = false;
                btnGuncelle.Visible = false;

                // Textboxları kilitle
                txtAd.ReadOnly = true;
                txtYazar.ReadOnly = true;
                txtISBN.ReadOnly = true;
                txtYayinevi.ReadOnly = true;
                txtBasimYili.ReadOnly = true;
                txtStok.ReadOnly = true;

                cmbAnaKategori.Enabled = false;
                cmbAltKategori.Enabled = false;
            }
        }

        private void KategoriVerileriniHazirla()
        {
            kategoriler.Clear();

            // 1. EDEBİYAT
            kategoriler.Add("1. EDEBİYAT (KURGU / SANATSAL)", new List<string> {
        "Roman",
        "Hikaye (Öykü)",
        "Tiyatro / Oyun",
        "Çizgi Roman / Manga / Grafik Roman",
        "Masal / Efsane"
    });

            // 2. KURGU DIŞI
            kategoriler.Add("2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)", new List<string> {
        "Tarih",
        "Felsefe & Düşünce",
        "Psikoloji",
        "Kişisel Gelişim",
        "Biyografi & Anı (Hatırat)",
        "Otobiyografi",
        "Gezi Yazısı (Seyahatname)",
        "Mektup / Günlük",
        "Din & İnançlar",
    });

            // 3. BİLİM VE TEKNİK
            kategoriler.Add("3. BİLİM VE TEKNİK", new List<string> {
        "Bilim",
        "Teknoloji & Mühendislik",
        
    });

            // 4. AKADEMİK VE MESLEKİ
            kategoriler.Add("4. AKADEMİK VE MESLEKİ", new List<string> {
        "Hukuk",
        "Ekonomi & İş Dünyası",
        "Siyaset Bilimi",
        "Tıp & Sağlık",
        "Eğitim Bilimleri"
    });

            // 5. YAŞAM VE HOBİ
            kategoriler.Add("5. YAŞAM VE HOBİ", new List<string> {
        "Sanat & Müzik",
        "Yemek Kültürü / Gastronomi",
        "Spor",
        "Dekorasyon / Moda"
    });

            // 6. YAŞ GRUPLARINA GÖRE
            kategoriler.Add("6. YAŞ GRUPLARINA GÖRE (Özel Koleksiyon)", new List<string> {
        "Çocuk Kitapları (0-6 Yaş / Okul Öncesi)",
        "İlk Gençlik (7-12 Yaş)",
        "Gençlik Edebiyatı (Young Adult)"
    });

            // 7. DİL VE EĞİTİM
            kategoriler.Add("7. DİL VE EĞİTİM", new List<string> {
        "Sözlükler / Ansiklopediler",
        "Yabancı Dil Öğrenimi",
        "Sınav Hazırlık (TYT / AYT / KPSS / LGS)"
    });
        
        }

        // ANA KATEGORİ SEÇİLİNCE (Otomatik Alt Kategori Doldurma) 
        private void cmbAnaKategori_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cmbAnaKategori.SelectedIndex == -1 || cmbAnaKategori.SelectedItem == null)
            {
                cmbAltKategori.DataSource = null;
                cmbAltKategori.Enabled = false;
                return;
            }

            try
            {
                if (cmbAnaKategori.SelectedItem is KeyValuePair<string, List<string>> secilenKategori)
                {
                    cmbAltKategori.DataSource = secilenKategori.Value;
                    cmbAltKategori.Enabled = true;
                    cmbAltKategori.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategori yüklenirken hata: " + ex.Message);
            }
        }

        // --- 4. LİSTELEME METODU ---
        private void Listele()
        {
            try
            {
                dgwKitaplar.DataSource = _bookService.TumKitaplariGetir();

                dgwKitaplar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgwKitaplar.ColumnHeadersHeight = 45;

                // Sütunları ekrana yay
                dgwKitaplar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgwKitaplar.ClearSelection();

                if (dgwKitaplar.Columns["Title"] != null) dgwKitaplar.Columns["Title"].HeaderText = "Kitap Adı";
                if (dgwKitaplar.Columns["Author"] != null) dgwKitaplar.Columns["Author"].HeaderText = "Yazar";
                if (dgwKitaplar.Columns["ISBN"] != null) dgwKitaplar.Columns["ISBN"].HeaderText = "ISBN";
                if (dgwKitaplar.Columns["Publisher"] != null) dgwKitaplar.Columns["Publisher"].HeaderText = "Yayınevi";
                if (dgwKitaplar.Columns["PublishYear"] != null) dgwKitaplar.Columns["PublishYear"].HeaderText = "Basım Yılı";
                if (dgwKitaplar.Columns["Stock"] != null) dgwKitaplar.Columns["Stock"].HeaderText = "Stok";
                if (dgwKitaplar.Columns["PageCount"] != null) dgwKitaplar.Columns["PageCount"].HeaderText = "Sayfa Sayısı";
                if (dgwKitaplar.Columns["Category"] != null) dgwKitaplar.Columns["Category"].HeaderText = "Ana Kategori";
                if (dgwKitaplar.Columns["SubCategory"] != null) dgwKitaplar.Columns["SubCategory"].HeaderText = "Alt Kategori";
                if (dgwKitaplar.Columns["IsAvailable"] != null) dgwKitaplar.Columns["IsAvailable"].HeaderText = "Müsait";

                // --- 3. GİZLENECEKLER ---
                if (dgwKitaplar.Columns["BookId"] != null) dgwKitaplar.Columns["BookId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme Hatası: " + ex.Message);
            }
        }

        // --- 5. KAYDET (EKLE) BUTONU ---
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                Book yeniKitap = new Book();

                // Formdaki Kutular -> Book Nesnesi
                yeniKitap.ISBN = txtISBN.Text;
                yeniKitap.Title = txtAd.Text;
                yeniKitap.Author = txtYazar.Text;
                yeniKitap.Publisher = txtYayinevi.Text;

                if (!string.IsNullOrEmpty(txtBasimYili.Text))
                    yeniKitap.PublishYear = Convert.ToInt32(txtBasimYili.Text);

                if (!string.IsNullOrEmpty(txtStok.Text))
                    yeniKitap.Stock = Convert.ToInt32(txtStok.Text);

                if (!string.IsNullOrEmpty(txtSayfaSayisi.Text))
                    yeniKitap.PageCount = int.Parse(txtSayfaSayisi.Text);

                if (cmbAnaKategori.SelectedItem != null)
                    yeniKitap.Category = ((KeyValuePair<string, List<string>>)cmbAnaKategori.SelectedItem).Key;
                else
                    yeniKitap.Category = "-";

                if (cmbAltKategori.SelectedItem != null)
                    yeniKitap.SubCategory = cmbAltKategori.SelectedItem.ToString();
                else
                    yeniKitap.SubCategory = "-";

                yeniKitap.IsAvailable = true;

                _bookService.KitapEkle(yeniKitap);

                MessageBox.Show("Kitap başarıyla eklendi!");
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme Hatası: " + ex.Message);
            }
        }

        // --- 6. TABLOYA TIKLAMA (Verileri Kutulara Doldur) ---
        private void dgwKitaplar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgwKitaplar.Rows[e.RowIndex];

                    _secilenKitapId = Convert.ToInt32(row.Cells["BookId"].Value);

                    txtISBN.Text = row.Cells["ISBN"].Value.ToString();
                    txtAd.Text = row.Cells["Title"].Value.ToString();
                    txtYazar.Text = row.Cells["Author"].Value.ToString();
                    txtYayinevi.Text = row.Cells["Publisher"].Value.ToString();
                    txtBasimYili.Text = row.Cells["PublishYear"].Value.ToString();
                    txtStok.Text = row.Cells["Stock"].Value.ToString();
                    txtSayfaSayisi.Text = row.Cells["PageCount"].Value.ToString();

                    // ComboBoxları Doldurma 
                    string anaKat = row.Cells["Category"].Value.ToString();
                    string altKat = "";

                    if (row.Cells["SubCategory"].Value != null)
                        altKat = row.Cells["SubCategory"].Value.ToString();

                    // Ana kategoriyi bul ve seç
                    int anaIndex = cmbAnaKategori.FindStringExact(anaKat);
                    if (anaIndex != -1)
                    {
                        cmbAnaKategori.SelectedIndex = anaIndex;
                        int altIndex = cmbAltKategori.FindStringExact(altKat);
                        if (altIndex != -1) cmbAltKategori.SelectedIndex = altIndex;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Seçim hatası: " + ex.Message);
                }
            }
        }

        // --- 7. GÜNCELLE BUTONU ---
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (_secilenKitapId == 0)
                {
                    MessageBox.Show("Lütfen listeden güncellenecek kitabı seçin.");
                    return;
                }

                Book guncelKitap = new Book();
                guncelKitap.BookId = _secilenKitapId;
                guncelKitap.ISBN = txtISBN.Text;
                guncelKitap.Title = txtAd.Text;
                guncelKitap.Author = txtYazar.Text;
                guncelKitap.Publisher = txtYayinevi.Text;


                if (!string.IsNullOrEmpty(txtBasimYili.Text))
                    guncelKitap.PublishYear = Convert.ToInt32(txtBasimYili.Text);

                if (!string.IsNullOrEmpty(txtStok.Text))
                    guncelKitap.Stock = Convert.ToInt32(txtStok.Text);

                if (!string.IsNullOrEmpty(txtSayfaSayisi.Text))
                    guncelKitap.PageCount = Convert.ToInt32(txtSayfaSayisi.Text);
               
                if (cmbAnaKategori.SelectedItem != null)
                    guncelKitap.Category = ((KeyValuePair<string, List<string>>)cmbAnaKategori.SelectedItem).Key;

                if (cmbAltKategori.SelectedItem != null)
                    guncelKitap.SubCategory = cmbAltKategori.SelectedItem.ToString();
                else
                    guncelKitap.SubCategory = "-"; // Alt kategori seçilmediyse tire koyar

                _bookService.KitapGuncelle(guncelKitap);

                MessageBox.Show("Kitap güncellendi.");
                Listele();
                Temizle();
                _secilenKitapId = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message);
            }
        }

        // --- 8. SİL BUTONU ---
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (_secilenKitapId == 0)
                {
                    MessageBox.Show("Lütfen listeden silinecek kitabı seçin.");
                    return;
                }

                var cevap = MessageBox.Show("Bu kitabı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (cevap == DialogResult.Yes)
                {
                    _bookService.KitapSil(_secilenKitapId);
                    MessageBox.Show("Kitap silindi.");
                    Listele();
                    Temizle();
                    _secilenKitapId = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme Hatası: " + ex.Message);
            }
        }

        // --- 9. TEMİZLEME YARDIMCISI ---
        private void Temizle()
        {
            txtISBN.Clear();
            txtAd.Clear();
            txtYazar.Clear();
            txtYayinevi.Clear();
            txtBasimYili.Clear();
            txtStok.Clear();
            txtSayfaSayisi.Clear();
           
            cmbAnaKategori.SelectedIndex = -1;
            cmbAltKategori.SelectedIndex = -1;
            cmbAltKategori.Enabled = false;
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtArama.Text))
            {
                Listele();
            }
            else
            {
                dgwKitaplar.DataSource = _bookService.KitapAra(txtArama.Text);
            }
        }


      
    }

} 

 


