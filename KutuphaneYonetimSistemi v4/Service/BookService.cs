using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KutuphaneYonetimSistemi_v4.DAL;
using KutuphaneYonetimSistemi_v4.Domain;

namespace KutuphaneYonetimSistemi_v4.Service
{
    public class BookService
    {
        private BookDao _bookDao;

        public BookService()
        {
            _bookDao = new BookDao();
        }

        // 1. TÜM KİTAPLARI GETİR
        public List<Book> TumKitaplariGetir()
        {
            return _bookDao.GetAllBooks();
        }

        // 2. KİTAP EKLEME
        public void KitapEkle(Book book)
        {
            
            if (string.IsNullOrEmpty(book.Title))
            {
                throw new Exception("Kitap adı boş olamaz!");
            }

            // Yazar kontrolü 
            if (string.IsNullOrEmpty(book.Author))
            {
                throw new Exception("Yazar adı boş olamaz!");
            }

            _bookDao.AddBook(book);
        }

        // 3. KİTAP SİLME
        public void KitapSil(int bookId)
        {
            _bookDao.DeleteBook(bookId);
        }

        // 4. KİTAP GÜNCELLEME
        public void KitapGuncelle(Book book)
        {
            
            if (string.IsNullOrEmpty(book.Title)) 
            {
                throw new Exception("Kitap adı boş olamaz!");
            }

            _bookDao.Update(book);
        }

        // 5. ARAMA
        public List<Book> KitapAra(string kelime)
        {
            return _bookDao.Search(kelime);
        }
    }
}
