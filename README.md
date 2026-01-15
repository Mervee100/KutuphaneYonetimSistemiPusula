# 🧭 PUSULA - Kütüphane Yönetim Sistemi

> **"Aradığın Kitapsa, Yönün Pusula"**

Bu proje, bir kütüphanedeki kitapların, üyelerin ve ödünç verme işlemlerinin dijital ortamda profesyonelce takip edilmesini sağlayan, **N-Katmanlı Mimari** yapısına uygun olarak geliştirilmiş bir **C# Windows Forms** uygulamasıdır.

Ayrıca, verileri anlık grafikler üzerinden analiz etmeye olanak sağlayan dinamik bir Dashboard (Raporlama) yapısına sahiptir.

---

## 📸 Ekran Görüntüleri

| Giriş Ekranı | 
| :---: | 
| <img width="401" height="541" alt="login" src="https://github.com/user-attachments/assets/4d08a1e3-2d7e-46f0-a8d3-7a530e7c1450" />

|Ana Menü |
| :---: | 
|<img width="465" height="622" alt="anamenü" src="https://github.com/user-attachments/assets/03b8019c-7d42-4a82-ba4a-8846609ded43" />


| Raporlama Dashboard | 
| :---: | 
|<img width="1717" height="898" alt="rapor" src="https://github.com/user-attachments/assets/8a08cc61-0972-44ce-a434-e8cad405f0e9" />

| Kitap Yönetimi |
| :---: | 
|<img width="1398" height="755" alt="kitap" src="https://github.com/user-attachments/assets/358f5b1e-970e-4ede-a61b-39fbf4f7c30b" />


| Üye Yönetimi |
| :---: |
|<img width="1360" height="774" alt="uye" src="https://github.com/user-attachments/assets/080d7075-2497-4372-a571-1b07213f3e71" />

Ödünç İşlemleri 
| :---: |
|<img width="1557" height="732" alt="odunc" src="https://github.com/user-attachments/assets/3700fac6-08d5-4a11-9323-4ec5c24264ad" />


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
* **E-posta:** [merve18062006@gmail.com]

---
▶ **YouTube Proje Videosu:** *(https://youtu.be/kBe3Xlegceg)*
