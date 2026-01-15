# 🧭 PUSULA - Kütüphane Yönetim Sistemi

> **"Aradığın Kitapsa, Yönün Pusula"**

Bu proje, bir kütüphanedeki kitapların, üyelerin ve ödünç verme işlemlerinin dijital ortamda profesyonelce takip edilmesini sağlayan, **N-Katmanlı Mimari** yapısına uygun olarak geliştirilmiş bir **C# Windows Forms** uygulamasıdır.

Özellikle yöneticilerin kütüphane verilerini analiz etmesini sağlayan gelişmiş **Raporlama Modülü** ile öne çıkmaktadır.

---

## 📸 Ekran Görüntüleri

| Giriş Ekranı | Ana Menü |
| :---: | :---: |
| <img src="<img width="401" height="541" alt="login" src="https://github.com/user-attachments/assets/2eb485f9-e623-4eee-bd74-dc11216b3bdf" />
in.png" width="400"> | <img src="<img width="465" height="622" alt="anamenü" src="https://github.com/user-attachments/assets/8362ed4b-cc4f-4ded-8d86-da701bf37603" /> width="400"> |

| Raporlama Dashboard | Kitap Yönetimi |
| :---: | :---: |
| <img src="EkranGoruntuleri/rapor.png" width="400"> | <img src="EkranGoruntuleri/kitap.png" width="400"> |<img width="1717" height="898" alt="rapor" src="https://github.com/user-attachments/assets/680be761-7093-4431-8af9-c77e08ab2ca0" /><img width="1398" height="755" alt="kitap" src="https://github.com/user-attachments/assets/7b79ffb6-134a-4ced-9591-96c1c82761a5" />



| Üye Yönetimi | Ödünç İşlemleri |
| :---: | :---: |
| <img src="EkranGoruntuleri/uye.png" width="400"> | <img src="EkranGoruntuleri/odunc.png" width="400"> |<img width="1360" height="774" alt="uye" src="https://github.com/user-attachments/assets/8196d93b-a4ae-4889-b2c6-240709fd9bad" /><img width="1557" height="732" alt="odunc" src="https://github.com/user-attachments/assets/5ee35129-d1f5-457e-8bbe-06c64168e7ae" />



---

## ✨ Özellikler

Bu proje kullanıcı rolleri (Yönetici & Personel) temel alınarak geliştirilmiştir:

### 📚 1. Kitap Yönetimi
* Kitap Ekleme, Silme ve Güncelleme işlemleri.
* Barkod/ISBN, Yazar, Yayınevi ve Stok takibi.
* Kitapları Ana Kategori ve Alt Kategoriye göre sınıflandırma.
* Dinamik arama 

### 👥 2. Üye (Okuyucu) Yönetimi
* Yeni üye kaydı ve üye bilgilerinin düzenlenmesi.

### 🔄 3. Ödünç & İade İşlemleri
* Üyelere kitap ödünç verme ve teslim tarihi belirleme.
* Stokların ödünç durumuna göre otomatik güncellenmesi.

### 📊 4. Gelişmiş Raporlama (Dashboard)
* **Anlık İstatistikler:** Toplam Kitap, Toplam Üye, Emanetteki Kitaplar, Gecikenler.
* **Grafiksel Analizler:**
    * 📉 **Aylık Ödünç Grafiği:** Hangi ayda ne kadar kitap verildiğini gösteren çizgi grafik.
    * 🍩 **Kategori Dağılımı:** Kitapların türlerine göre dağılımını gösteren pasta grafiği.
    * 📊 **Yaş Dağılımı:** Üyelerin yaş gruplarına (Çocuk, Genç, Yetişkin) göre analizi.
* **Listeler:** En son eklenen kitaplar, en çok okuyan üyeler ,toplam kitap,toplam üye,emanetteki kitaplar,en çok okunan kitaplar,geciken kitaplar,popüler yazarlar ve pasif üyeler.

---

## 🛠️ Kullanılan Teknolojiler ve Mimari

Proje, sürdürülebilirlik ve temiz kod prensipleri gözetilerek **N-Katmanlı Mimari (N-Tier Architecture)** ile geliştirilmiştir.

* **Dil:** C# (.NET Framework)
* **Arayüz:** Windows Forms (WinForms) & MS Charting
* **Veritabanı:** MySQL
* **Mimari:**
    * **Entity Layer:** Veritabanı tablolarına karşılık gelen nesneler.
    * **DAL (Data Access Layer):** Veritabanı bağlantısı ve CRUD işlemleri.
    * **BLL (Business Logic Layer):** İş kuralları ve veri doğrulama.
    * **UI (User Interface):** Kullanıcı arayüzü formları.

---

## 🚀 Kurulum

1.  Projeyi bilgisayarınıza indirin.
2.  **Veritabanı Kurulumu:**
    * Proje dosyasının içindeki `KutuphaneDb.sql` dosyasını MySQL (phpMyAdmin) üzerinden içe aktarın (Import).
3.  **Bağlantı Ayarı:**
    * Projeyi Visual Studio ile açın.
    * MySQL bağlantı bilgilerini kendi sunucunuza göre güncelleyin.
4.  Projeyi başlatın.

---

## 📞 İletişim

* **Geliştirici:** [Merve Yılmaz]
* **LinkedIn:** [Linkedin Profil Linkiniz]
* **E-posta:** [merve18062006@gmail.com]

---
▶ **YouTube Proje Videosu:** *(https://youtu.be/kBe3Xlegceg)*
