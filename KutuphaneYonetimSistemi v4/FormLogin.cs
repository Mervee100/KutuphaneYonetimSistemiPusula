using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KutuphaneYonetimSistemi_v4.Service;
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4
{
    public partial class FormLogin : Form
    {
        private UserService _userService;

        public FormLogin()
        {
            InitializeComponent();
            _userService = new UserService();

            txtSifre.PasswordChar = '*';
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kadi = txtKullaniciAdi.Text.Trim(); // Boşlukları temizler
            string sifre = txtSifre.Text.Trim();

            if (string.IsNullOrEmpty(kadi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.");
                return;
            }

            // Veritabanında böyle biri var mı?
            User kullanici = _userService.GirisYap(kadi, sifre);

            if (kullanici != null)
            {
                // ID -> İSİM
                string yetkiAdi = "";
                if (kullanici.RoleId == 1) yetkiAdi = "Yönetici";
                else if (kullanici.RoleId == 2) yetkiAdi = "Personel";
                else if (kullanici.RoleId == 3) yetkiAdi = "Üye";
                else yetkiAdi = "Tanımsız";

                MessageBox.Show("Giriş Başarılı!\nHoşgeldin: " + kullanici.Username + "\nYetki: " + yetkiAdi);

                FormAnaMenu anaMenu = new FormAnaMenu(kullanici);
                this.Hide();
                anaMenu.ShowDialog();

                Application.Exit();
            }
            else
            {
                // HATALI GİRİŞ MESAJI
                MessageBox.Show("Şifre veya kullanıcı adı hatalı!");
            }
        }

    }
}

