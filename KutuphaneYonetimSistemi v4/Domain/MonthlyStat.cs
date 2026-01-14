using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi_v4.Domain
{
    public class MonthlyStat
    {
        public string Ay { get; set; }      
        public int IslemSayisi { get; set; } // O ay kaç kitap verilmiş?
    }
}
