using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi_v4.Domain
{
    public class Borrow
    {
        public int BorrowId { get; set; }      
        public int BookId { get; set; }        
        public int MemberId { get; set; }      
        public DateTime BorrowDate { get; set; } // Alış Tarihi
        public DateTime DueDate { get; set; }    // Teslim Etmesi Gereken Tarih
        public DateTime? ReturnDate { get; set; } // İade Tarihi 
    }
}
