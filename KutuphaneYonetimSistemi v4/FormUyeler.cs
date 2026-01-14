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
    public partial class FormUyeler : Form
    {
        private MemberService _memberService;
        private int _secilenUyeId = 0;
        public FormUyeler()
        {
            InitializeComponent();
            _memberService = new MemberService();
        }
        private void Listele()
        {
            dgwUyeler.DataSource = _memberService.GetAll();
            dgwUyeler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Temizle()
        {
            txtAd.Clear(); txtSoyad.Clear(); txtTelefon.Clear(); txtEposta.Clear();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Member uye = new Member();
                uye.Ad = txtAd.Text;
                uye.Soyad = txtSoyad.Text;
                uye.Telefon = txtTelefon.Text;
                uye.Eposta = txtEposta.Text;
                uye.DogumTarihi = dtpDogumTarihi.Value;

                _memberService.UyeEkle(uye);
                MessageBox.Show("Üye Eklendi!");
                Listele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (_secilenUyeId == 0) { MessageBox.Show("Seçim yapın"); return; }

                if (MessageBox.Show("Silinsin mi?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _memberService.UyeSil(_secilenUyeId);
                    Listele();
                    Temizle();
                    _secilenUyeId = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (_secilenUyeId == 0) { MessageBox.Show("Seçim yapın"); return; }

                Member uye = new Member();
                uye.Id = _secilenUyeId;
                uye.Ad = txtAd.Text;
                uye.Soyad = txtSoyad.Text;
                uye.Telefon = txtTelefon.Text;
                uye.Eposta = txtEposta.Text;
                uye.DogumTarihi = dtpDogumTarihi.Value;

                _memberService.UyeGuncelle(uye);
                MessageBox.Show("Üye Güncellendi!");
                Listele();
                Temizle();
                _secilenUyeId = 0;
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void dgwUyeler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgwUyeler.Rows[e.RowIndex];
                _secilenUyeId = Convert.ToInt32(row.Cells["Id"].Value);
                txtAd.Text = row.Cells["Ad"].Value.ToString();
                txtSoyad.Text = row.Cells["Soyad"].Value.ToString();
                txtTelefon.Text = row.Cells["Telefon"].Value.ToString();
                txtEposta.Text = row.Cells["Eposta"].Value.ToString();

                if (row.Cells["DogumTarihi"].Value != null && row.Cells["DogumTarihi"].Value != DBNull.Value)
                {
                    dtpDogumTarihi.Value = Convert.ToDateTime(row.Cells["DogumTarihi"].Value);
                }
                else
                {
                    MessageBox.Show("Seçilen üyenin doğum tarihi sistemde kayıtlı değil! Lütfen güncelleyin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDogumTarihi.Value = DateTime.Now;
                }

            }
        }

        private void FormUyeler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtArama.Text)) Listele();
            else dgwUyeler.DataSource = _memberService.UyeAra(txtArama.Text);
        }
    }
}
