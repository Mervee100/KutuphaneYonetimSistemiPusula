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

namespace KutuphaneYonetimSistemi_v4
{
    public partial class FormAnaMenu : Form
    {
        
        private User _girisYapanKullanici;
        public FormAnaMenu(User kullanici)
        {
            InitializeComponent();
            _girisYapanKullanici = kullanici;
        }
        
        
        // KİTAP İŞLEMLERİ BUTONU
        private void btnKitaplar_Click(object sender, EventArgs e)
        {
            FormKitap kitapSayfasi = new FormKitap(_girisYapanKullanici.RoleId);
            kitapSayfasi.ShowDialog();
        }

        // ÜYE İŞLEMLERİ BUTONU
        private void btnUyeler_Click(object sender, EventArgs e)
        {
            FormUyeler uyeSayfasi = new FormUyeler();
            uyeSayfasi.ShowDialog(); // Üye sayfasını aç
        }

        // ÇIKIŞ BUTONU
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Programı tamamen kapat
        }
        private void FormAnaMenu_Load(object sender, EventArgs e)
        {
            // Kullanıcı boş gelirse hata vermesin diye kontrol
            if (_girisYapanKullanici == null) return;

            int rol = _girisYapanKullanici.RoleId;

            // --- YETKİ AYARLARI ---

            // 1. YÖNETİCİ (Admin) - RoleId: 1
            if (rol == 1)
            {
                // Yönetici her şeyi görür 
                btnKitaplar.Visible = true;
                btnUyeler.Visible = true;
                btnOdunc.Visible = true;
                btnRaporlar.Visible = true;
            }
            // 2. PERSONEL (Görevli) - RoleId: 2
            else if (rol == 2)
            {
                // Görevli: Kitap, Üye ve Ödünç işlemlerini yapar 
                btnKitaplar.Visible = true;
                btnUyeler.Visible = true;
                btnOdunc.Visible = true;

                btnRaporlar.Visible = false; // Rapor butonunu gizle
            }
            // 3. ÜYE (Okuyucu) - RoleId: 3
            else if (rol == 3)
            {
                // Üye: Sadece kitapları görebilir 
                // Başka üye ekleyemez, kendine ödünç işlemi başlatamaz (bunu görevli yapar).
                btnKitaplar.Visible = true; // Kitap listesine bakabilir

                btnUyeler.Visible = false;   
                btnOdunc.Visible = false;    
                btnRaporlar.Visible = false; 
            }
        }

        private void btnOdunc_Click(object sender, EventArgs e)
        {
            FormOdunc frm = new FormOdunc();
            frm.ShowDialog();
        }

        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            FormRapor frm = new FormRapor();
            frm.ShowDialog();
        }
    }
}
