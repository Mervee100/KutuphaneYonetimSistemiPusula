using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi_v4.Domain
{
    public class Member : Kisi
    {
        public DateTime UyelikTarihi { get; set; }
        public override string ToString()
        {
            return Ad + " " + Soyad;
        }

    }
}