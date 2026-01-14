using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi_v4.DAL;
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4.Service
{
    public class UserService
    {
        private UserDao _userDao;

        public UserService()
        {
            _userDao = new UserDao();
        }

        public User GirisYap(string kadi, string sifre)
        {
            return _userDao.KullaniciKontrolEt(kadi, sifre);
        }
    }
}
