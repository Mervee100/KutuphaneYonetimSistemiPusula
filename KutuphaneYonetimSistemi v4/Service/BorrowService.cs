using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi_v4.DAL;
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4.Service
{

    public class BorrowService
    {
            private BorrowDao _borrowDao;

            public BorrowService()
            {
                _borrowDao = new BorrowDao();
            }

            public void LendBook(int bookId, int memberId, DateTime dueDate)
            {
                if (_borrowDao.IsBookCurrentlyBorrowed(bookId))
                {
                    throw new Exception("Bu kitap şu an başkasında görünüyor, ödünç verilemez!");
                }

                Borrow borrow = new Borrow();
                borrow.BookId = bookId;
                borrow.MemberId = memberId;
                borrow.BorrowDate = DateTime.Now;
                borrow.DueDate = dueDate;
                borrow.ReturnDate = null;

                _borrowDao.Add(borrow);
            }

            public void ReceiveBook(int borrowId)
            {
                _borrowDao.UpdateReturnDate(borrowId);
            }

        public System.Collections.Generic.List<KutuphaneYonetimSistemi_v4.Domain.BorrowDetail> GetList()
        {
            return _borrowDao.GetBorrowDetails();
        }
    }
}
