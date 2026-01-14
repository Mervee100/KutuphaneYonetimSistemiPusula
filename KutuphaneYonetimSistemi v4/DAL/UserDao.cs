using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4.DAL
{
    public class UserDao
    {
        private string baglantiCumlesi = "Server=172.21.54.253;Database=26_132430071;Uid=26_132430071;Pwd=İnif123.;";

        public User KullaniciKontrolEt(string kadi, string sifre)
        {
            User user = null;
            using (MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

                // SQL'de User kelimesi ayrılmış bir kelime olabileceği için `User` şeklinde kullanmak güvenlidir.
                string sorgu = "SELECT * FROM `User` WHERE Username=@user AND PasswordHash=@pass";

                using (MySqlCommand komut = new MySqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@user", kadi);
                    komut.Parameters.AddWithValue("@pass", sifre);

                    using (MySqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        if (okuyucu.Read())
                        {
                            user = new User
                            {
                                UserId = Convert.ToInt32(okuyucu["UserId"]),
                                Username = okuyucu["Username"].ToString(),
                                RoleId = Convert.ToInt32(okuyucu["RoleId"])
                            };

                            // MemberId veritabanında NULL olabilir, kontrol ediyoruz
                            if (okuyucu["MemberId"] != DBNull.Value)
                            {
                                user.MemberId = Convert.ToInt32(okuyucu["MemberId"]);
                            }
                        }
                    }
                }
            }
            return user;
        }
    }
}
