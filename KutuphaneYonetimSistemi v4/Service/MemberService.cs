using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi_v4.DAL;
using KutuphaneYonetimSistemi_v4.Domain;
namespace KutuphaneYonetimSistemi_v4.Service
{
    public class MemberService
    {
        private MemberDao _memberDao;

        public MemberService()
        {
            _memberDao = new MemberDao();
        }

        public void UyeEkle(Member uye)
        {
            _memberDao.Add(uye);
        }
                                              
        public List<Member> GetAll()
        {
            return _memberDao.GetAll();
        }

        public void UyeSil(int id)
        {
            _memberDao.Delete(id);
        }

        public void UyeGuncelle(Member uye)
        {
            _memberDao.Update(uye);
        }

        public List<Member> UyeAra(string aranan)
        {
            return _memberDao.Search(aranan);
        }

        
    }
}
