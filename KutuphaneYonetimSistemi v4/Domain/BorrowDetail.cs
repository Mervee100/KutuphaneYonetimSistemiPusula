using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi_v4.Domain
{
     public class BorrowDetail
     {
        public int BorrowId { get; set; }
        public string KitapAdi { get; set; }  
        public string UyeAdi { get; set; }   
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
     }
}
