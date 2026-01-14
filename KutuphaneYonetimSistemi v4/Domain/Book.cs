using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi_v4.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int PublishYear { get; set; }
        public int Stock { get; set; }
        public int PageCount { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public bool IsAvailable { get; set; }
        public int BookStatus { get; set; }
    }
}
