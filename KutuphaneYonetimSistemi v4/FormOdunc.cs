using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KutuphaneYonetimSistemi_v4.Service; // Servis katmanını eklemeyi unutma
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4
{
    public partial class FormOdunc : Form
    {
        private BorrowService _borrowService;
        private MemberService _memberService;
        private BookService _bookService;

        public FormOdunc()
        {
            InitializeComponent();
            _borrowService = new BorrowService();
            _memberService = new MemberService();
            _bookService = new BookService();
        }

        private void FormOdunc_Load(object sender, EventArgs e)
        {
            try
            {
                ComboboxlariDoldur();
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yükleme hatası: " + ex.Message);
            }
           
            // 1. ÜYE SEÇİMİ İÇİN  ARAMA
            cmbUyeler.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Hem önerir hem tamamlar
            cmbUyeler.AutoCompleteSource = AutoCompleteSource.ListItems; // İçindeki listeden arar

            // 2. KİTAP SEÇİMİ İÇİN  ARAMA
            cmbKitaplar.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbKitaplar.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void ComboboxlariDoldur()
        {
            // --- 1. ÜYELERİ DOLDUR ---
            List<Member> uyeler = _memberService.GetAll();

            cmbUyeler.DataSource = null;
            cmbUyeler.DataSource = uyeler;
            cmbUyeler.ValueMember = "Id";   
            cmbUyeler.DisplayMember = "";   
            cmbUyeler.SelectedIndex = -1;


            // --- 2. KİTAPLARI DOLDUR ---
            List<Book> kitaplar = _bookService.TumKitaplariGetir();

            cmbKitaplar.DataSource = null; 
            cmbKitaplar.DataSource = kitaplar;

            cmbKitaplar.DisplayMember = "Title";
            cmbKitaplar.ValueMember = "BookId";

            cmbKitaplar.SelectedIndex = -1;
        }

        private void ListeyiYenile()
        {

            dgvOduncListesi.DataSource = null;
            dgvOduncListesi.DataSource = _borrowService.GetList();
            // --- 1. GÖRSEL AYARLAR (Sığmayan yazılar için) ---
            dgvOduncListesi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvOduncListesi.ColumnHeadersHeight = 45; 
            dgvOduncListesi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- 2. BAŞLIKLARI TÜRKÇELEŞTİRME ---
            
            if (dgvOduncListesi.Columns["KitapAdi"] != null) dgvOduncListesi.Columns["KitapAdi"].HeaderText = "Kitap Adı";
            else if (dgvOduncListesi.Columns["BookTitle"] != null) dgvOduncListesi.Columns["BookTitle"].HeaderText = "Kitap Adı";

            if (dgvOduncListesi.Columns["UyeAdi"] != null) dgvOduncListesi.Columns["UyeAdi"].HeaderText = "Üye Adı";
            else if (dgvOduncListesi.Columns["MemberName"] != null) dgvOduncListesi.Columns["MemberName"].HeaderText = "Üye Adı";

            if (dgvOduncListesi.Columns["BorrowDate"] != null) dgvOduncListesi.Columns["BorrowDate"].HeaderText = "Alış Tarihi";
            if (dgvOduncListesi.Columns["DueDate"] != null) dgvOduncListesi.Columns["DueDate"].HeaderText = "Son Teslim";
            if (dgvOduncListesi.Columns["ReturnDate"] != null) dgvOduncListesi.Columns["ReturnDate"].HeaderText = "İade Tarihi";

            // --- 3. GİZLENECEKLER ---
            if (dgvOduncListesi.Columns["BorrowId"] != null) dgvOduncListesi.Columns["BorrowId"].Visible = false;
        }

        // Ödünç Ver Butonu
        private void btnOduncVer_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUyeler.SelectedValue == null || cmbKitaplar.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen bir üye ve bir kitap seçiniz.");
                    return;
                }

                int uyeId = Convert.ToInt32(cmbUyeler.SelectedValue);
                int kitapId = Convert.ToInt32(cmbKitaplar.SelectedValue);

                DateTime tarih = dtpIadeTarihi.Value;

                _borrowService.LendBook(kitapId, uyeId, tarih);

                MessageBox.Show("Kitap başarıyla ödünç verildi!");

                ListeyiYenile();

                cmbUyeler.SelectedIndex = -1;
                cmbKitaplar.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // Teslim Al (İade) Butonu
        private void btnIadeAl_Click(object sender, EventArgs e)
        {
            if (dgvOduncListesi.SelectedRows.Count > 0)
            {
                try
                {
                    int borrowId = Convert.ToInt32(dgvOduncListesi.SelectedRows[0].Cells["BorrowId"].Value);

                    var returnDateCell = dgvOduncListesi.SelectedRows[0].Cells["ReturnDate"].Value;
                    if (returnDateCell != null && returnDateCell.ToString() != "")
                    {
                        MessageBox.Show("Bu kitap zaten teslim alınmış.");
                        return;
                    }

                    _borrowService.ReceiveBook(borrowId);
                    MessageBox.Show("Kitap teslim alındı.");
                    ListeyiYenile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İade işleminde hata: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen listeden teslim alınacak satırı seçin.");
            }
        }
       
        private void dgvOduncListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOduncListesi.Rows[e.RowIndex];
                cmbUyeler.Text = row.Cells["UyeAdi"].Value.ToString();
                cmbKitaplar.Text = row.Cells["KitapAdi"].Value.ToString();
            }
        }

    }

}
