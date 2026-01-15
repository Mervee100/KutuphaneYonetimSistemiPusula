-- phpMyAdmin SQL Dump
-- version 5.2.1deb3
-- https://www.phpmyadmin.net/
--
-- Anamakine: localhost:3306
-- Üretim Zamanı: 15 Oca 2026, 12:56:55
-- Sunucu sürümü: 8.0.44-0ubuntu0.24.04.1
-- PHP Sürümü: 8.3.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `26_132430071`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `B`
--

CREATE TABLE `B` (
  `BookId` int NOT NULL COMMENT 'Kategorinin benzersiz kimliği',
  `ISBN` varchar(20) NOT NULL COMMENT 'Kitabın ISBN numarası',
  `Title` varchar(255) NOT NULL COMMENT 'Kitabın adı',
  `Author` varchar(100) NOT NULL COMMENT 'Yazar Adı',
  `Publisher` varchar(100) NOT NULL COMMENT 'Yayınevi',
  `PublisYear` int NOT NULL COMMENT 'Basım yılı',
  `Stock` int NOT NULL DEFAULT '0' COMMENT 'Kitap stok adedi',
  `CategoryId` int NOT NULL COMMENT 'Hangi kategoriye ait olduğu'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `Books`
--

CREATE TABLE `Books` (
  `BookId` int NOT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `Author` varchar(100) DEFAULT NULL,
  `PageCount` int DEFAULT NULL,
  `IsAvailable` tinyint(1) DEFAULT '1',
  `ISBN` varchar(50) NOT NULL,
  `Publisher` varchar(100) NOT NULL,
  `PublishYear` int NOT NULL,
  `Stock` int NOT NULL,
  `Category` varchar(100) DEFAULT NULL,
  `SubCategory` varchar(100) DEFAULT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `BookStatus` tinyint NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `Books`
--

INSERT INTO `Books` (`BookId`, `Title`, `Author`, `PageCount`, `IsAvailable`, `ISBN`, `Publisher`, `PublishYear`, `Stock`, `Category`, `SubCategory`, `CreatedDate`, `BookStatus`) VALUES
(3, 'Nutuk', 'Mustafa Kemal Atatürk', 543, 1, '9789759021234', 'Türk Tarih Kurumu', 2018, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Tarih', '2026-01-02 10:16:32', 0),
(5, 'Sefiller', 'Victor Hugo', 1000, 0, '9789753638025', 'İletişim Yayınları', 2017, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-02 10:16:32', 0),
(10, 'Simyacı', 'Paulo Coelho', 184, 1, '9789750726435', 'Can Yayınları', 2018, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-02 10:16:32', 0),
(11, 'Kalk Çalış Başarısız Ol', 'Behçet Yalın Özkara', 216, 1, '9786254413280', 'Destek Yayınları', 2022, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Kişisel Gelişim', '2026-01-02 10:16:32', 0),
(25, 'Sarı Yüz', 'R.F.Kuang', 360, 1, '9786253691221', 'İthaki Yayınları', 2023, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-02 10:16:32', 0),
(26, 'Kendine İyi Davren Güzel İnsan', 'Beyhan Budak', 0, 1, '9786053116050', 'Destek Yayınları', 2019, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-02 10:16:32', 0),
(27, 'Hayat Acemileri İçin Yaşam Rehberi', 'Beyhan Budak', 100, 1, '252689', 'Kronik Yayınları', 2025, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-02 10:16:32', 0),
(30, 'Suç ve Ceza', 'Fyodor Dostoyevski', 704, 1, '9789750719383', 'Can Yayınları', 2019, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-06 20:08:52', 0),
(31, '1984', 'George Orwell', 352, 1, '9789750718539', 'Can Yayınları', 2020, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-06 20:11:57', 0),
(32, 'Kürk Mantolu Madonna', 'Sabahattin Ali', 160, 1, '9789753638001', 'Yapı Kredi Yayınları', 2016, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-06 20:13:20', 0),
(33, 'Beyaz Zambaklar Ülkesinde', 'Grigory Petrov', 160, 1, '9789752445679', 'İnkılap Kitabevi', 2015, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Kişisel Gelişim', '2026-01-06 20:16:17', 0),
(34, 'Fareler ve İnsanlar', 'John Steinbeck', 112, 1, '9789754589029', 'Sel Yayıncılık', 2019, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-06 20:17:32', 0),
(35, 'Gülün Adı', 'Umberto Eco', 592, 1, '9789750512348', 'Can Yayınları', 2014, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Tarihi Roman', '2026-01-06 20:37:32', 0),
(36, 'Devlet', 'Platon', 368, 1, '9789754589012', 'İş Bankası Kültür Yayınları', 2013, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Felsefe & Düşünce', '2026-01-06 20:38:53', 0),
(37, 'Yapay Zeka: Modern Yaklaşımlar', 'Stuart Russell, Peter Norvig', 1132, 1, '9786052994567', 'Nobel Akademik Yayıncılık', 2021, 1, '3. BİLİM VE TEKNİK', 'Teknoloji & Mühendislik', '2026-01-06 20:40:18', 0),
(38, 'Çizginin Dışındakiler', 'Malcolm Gladwell', 320, 1, '9789750738605', 'MediaCat Yayınları', 2019, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-07 18:30:12', 0),
(39, 'Senin Suçun Değil', 'Beyhan Budak', 240, 1, '9786254413075', 'Destek Yayınları', 2021, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-07 18:32:29', 0),
(40, 'Gümüş Yürek', 'D.N. Arca', 416, 1, '9786053849156', 'Epsilon Yayınları', 2022, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:34:12', 0),
(41, 'Gümüş Yürek – Karanlığın Çağrısı', 'D.N. Arca', 432, 1, '9786053849163', 'Epsilon Yayınları', 2023, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:35:25', 0),
(42, 'Tutunamayanlar', 'Oğuz Atay', 724, 1, '9789753638018', 'İletişim Yayınları', 2016, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:36:38', 0),
(43, 'Körlük', 'José Saramago', 352, 1, '9789750726848', 'Can Yayınları', 2018, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:37:50', 0),
(44, 'İnsan Neyle Yaşar?', 'Lev Tolstoy', 120, 1, '9789754589036', 'İş Bankası Kültür Yayınları', 2015, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Hikaye (Öykü)', '2026-01-07 18:39:01', 0),
(45, 'Yöneticinin El Kitabı', 'Harvard Business Review', 304, 1, '9786052587424', 'Optimist Yayınları', 2018, 1, '4. AKADEMİK VE MESLEKİ', 'Ekonomi & İş Dünyası', '2026-01-07 18:50:05', 0),
(46, 'Masumiyet Müzesi', 'Orhan Pamuk', 592, 1, '9789750820669', 'Yapı Kredi Yayınları', 2017, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:51:09', 0),
(47, 'İş Analistinin El Kitabı', 'Howard Podeswa', 416, 1, '9786051336238', 'Abaküs Kitap', 2020, 1, '4. AKADEMİK VE MESLEKİ', 'Ekonomi & İş Dünyası', '2026-01-07 18:52:13', 0),
(48, 'Dijital Dönüşüm ve Liderlik', 'George Westerman, Didier Bonnet, Andrew McAfee', 304, 1, '9786052587455', 'Optimist Yayınları', 2019, 1, '3. BİLİM VE TEKNİK', 'Teknoloji & Mühendislik', '2026-01-07 18:53:23', 0),
(49, 'Anna Karenina', 'Lev Tolstoy', 864, 1, '9789754589005', 'İş Bankası Kültür Yayınları', 2016, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:56:43', 0),
(50, 'Dönüşüm', 'Franz Kafka', 96, 1, '9789754589019', 'İş Bankası Kültür Yayınları', 2018, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 18:58:13', 0),
(51, 'Yeraltından Notlar', 'Fyodor Dostoyevski', 176, 1, '9789750719475', 'Can Yayınları', 2017, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 19:00:02', 0),
(52, 'Saatleri Ayarlama Enstitüsü', 'Ahmet Hamdi Tanpınar', 382, 1, '9789753638022', 'Dergâh Yayınları', 2019, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 19:02:01', 0),
(53, 'Atomik Alışkanlıklar', 'James Clear', 352, 1, '9786051982428', 'Pegasus Yayınları', 2021, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Kişisel Gelişim', '2026-01-07 19:03:01', 0),
(54, 'İnsanın Anlam Arayışı', 'Viktor E. Frankl', 168, 1, '9789750718521', 'Okuyan Us Yayınları', 2020, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-07 19:04:11', 0),
(55, 'Alışkanlıkların Gücü', 'Charles Duhigg', 408, 1, '9786050928656', 'Boyner Yayınları', 2019, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-07 19:05:16', 0),
(56, 'Sapiens: Hayvanlardan Tanrılara', 'Yuval Noah Harari', 416, 1, '9786050928663', 'Kolektif Kitap', 2022, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Tarih', '2026-01-07 19:06:33', 0),
(57, 'Cesur Yeni Dünya', 'Aldous Huxley', 312, 1, '9789750718546', 'Can Yayınları', 2021, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-07 19:07:51', 0),
(58, 'Hayvanlardan Tanrılara', 'Yuval Noah Harari', 248, 1, '9786050928670', 'Kolektif Kitap', 2023, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Çizgi Roman / Manga / Grafik Roman', '2026-01-07 19:09:16', 0),
(59, 'Homo Deus', 'Yuval Noah Harari', 464, 1, '9786050928687', 'Kolektif Kitap', 2021, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Felsefe & Düşünce', '2026-01-07 19:11:13', 0),
(60, 'Dijital Minimalizm', 'Cal Newport', 304, 1, '9786051982435', 'Metropolis Yayınları', 2020, 1, '3. BİLİM VE TEKNİK', 'Teknoloji & Mühendislik', '2026-01-07 19:12:23', 0),
(61, 'Don Kişot', 'Miguel de Cervantes', 1024, 1, '9789754589104', 'İş Bankası Kültür Yayınları', 2015, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:38:34', 0),
(62, 'Madame Bovary', 'Gustave Flaubert', 408, 1, '9789750719604', 'Can Yayınları', 2018, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:39:33', 0),
(63, 'Karamazov Kardeşler', 'Fyodor Dostoyevski', 824, 1, '9789750719611', 'Can Yayınları', 2020, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:40:29', 0),
(64, 'Uğultulu Tepeler', 'Emily Brontë', 416, 1, '9789754589111', 'İş Bankası Kültür Yayınları', 2017, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:41:33', 0),
(65, 'Otomatik Portakal', 'Anthony Burgess', 192, 1, '9789750719628', 'Can Yayınları', 2019, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:42:26', 0),
(66, 'Ben, Robot', 'Isaac Asimov', 264, 1, '9789754589128', 'İthaki Yayınları', 2021, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:43:23', 0),
(67, 'Fahrenheit 451', 'Ray Bradbury', 256, 1, '9789750719635', 'Can Yayınları', 2022, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:44:25', 0),
(68, 'Küçük Prens', 'Antoine de Saint-Exupéry', 112, 1, '9789754589135', 'Can Yayınları', 2016, 1, '6. YAŞ GRUPLARINA GÖRE (Özel Koleksiyon)', 'İlk Gençlik (7-12 Yaş)', '2026-01-09 12:45:21', 0),
(69, 'Hayvan Çiftliği', 'George Orwell', 152, 1, 'Hayvan Çiftliği', 'Can Yayınları', 2021, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:46:26', 0),
(70, 'Vadideki Zambak', 'Honoré de Balzac', 384, 1, '9789754589142', 'İş Bankası Kültür Yayınları', 2014, 1, '1. EDEBİYAT (KURGU / SANATSAL)', 'Roman', '2026-01-09 12:47:22', 0),
(71, 'Düşün ve Zengin Ol', 'Napoleon Hill', 304, 1, '9786051982503', 'Koridor Yayıncılık', 2020, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Kişisel Gelişim', '2026-01-09 12:48:34', 0),
(72, 'Akış (Flow)', 'Mihaly Csikszentmihalyi', 368, 1, '9786051982510', 'Buzdağı Yayınları', 2019, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-09 12:49:28', 0),
(73, 'İknanın Psikolojisi', 'Robert B. Cialdini ', 432, 1, '9786051982527', 'MediaCat', 2021, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Psikoloji', '2026-01-09 12:51:01', 0),
(74, 'Derin Çalışma', 'Cal Newport', 296, 1, '9786051982534', 'Metropolis Yayınları', 2020, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Kişisel Gelişim', '2026-01-09 12:52:04', 0),
(75, 'Zamanın Kısa Tarihi', 'Stephen Hawking', 212, 1, '9789754589159', 'Alfa Yayınları', 2018, 1, '3. BİLİM VE TEKNİK', 'Bilim', '2026-01-09 12:53:18', 0),
(76, 'Kozmos', 'Carl Sagan', 384, 1, '9789754589166', 'Alfa Yayınları', 2019, 1, '3. BİLİM VE TEKNİK', 'Bilim', '2026-01-09 12:55:03', 0),
(77, 'Homo Sapiens’in Kısa Tarihi', 'Yuval Noah Harari', 416, 1, '9786051982541', 'Kolektif Kitap', 2023, 1, '2. KURGU DIŞI (ÖĞRETİCİ / BİLGİSEL)', 'Tarih', '2026-01-09 12:56:08', 0),
(78, 'Dijital Çağda İnsan', 'Sherry Turkle', 352, 1, '9786051982558', 'Metis Yayınları', 2021, 1, '3. BİLİM VE TEKNİK', 'Bilim', '2026-01-09 12:57:05', 0),
(79, 'Yapay Zeka ve Gelecek', 'Max Tegmark', 400, 1, '9786051982565', 'Alfa Yayınları', 2022, 1, '3. BİLİM VE TEKNİK', 'Teknoloji & Mühendislik', '2026-01-09 12:58:07', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `Borrows`
--

CREATE TABLE `Borrows` (
  `BorrowId` int NOT NULL COMMENT 'ödünç işleminin benzersiz kimliği',
  `BookId` int NOT NULL COMMENT 'ödünç alınan kitap',
  `MemberId` int NOT NULL COMMENT 'kitabı ödünç alan üye',
  `BorrowDate` date NOT NULL COMMENT 'Kitabın ödünç alındığı tarih',
  `DueDate` date NOT NULL COMMENT 'kitabın teslim edilmesi gereken tarih',
  `ReturnDate` date DEFAULT NULL COMMENT 'Kitabın iade edildiği tarih(iade edilmediyse Null)'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `Borrows`
--

INSERT INTO `Borrows` (`BorrowId`, `BookId`, `MemberId`, `BorrowDate`, `DueDate`, `ReturnDate`) VALUES
(15, 52, 2, '2026-01-08', '2026-01-23', NULL),
(16, 11, 13, '2026-01-08', '2026-01-23', NULL),
(17, 3, 15, '2026-01-08', '2026-01-23', NULL),
(18, 5, 18, '2026-01-08', '2026-01-23', NULL),
(19, 10, 19, '2026-01-08', '2026-01-23', NULL),
(20, 25, 20, '2026-01-08', '2026-01-23', NULL),
(21, 26, 21, '2026-01-08', '2026-01-23', NULL),
(22, 27, 22, '2026-01-08', '2026-01-23', NULL),
(23, 30, 23, '2026-01-08', '2026-01-23', '2026-01-08'),
(24, 31, 24, '2026-01-08', '2026-01-23', NULL),
(25, 32, 2, '2026-01-08', '2026-01-23', NULL),
(26, 33, 25, '2026-01-08', '2026-01-09', NULL),
(27, 34, 26, '2026-01-08', '2026-01-09', '2026-01-09'),
(28, 35, 27, '2026-01-08', '2026-01-10', '2026-01-14'),
(29, 36, 28, '2026-01-08', '2025-12-30', '2026-01-14'),
(30, 37, 29, '2026-01-08', '2026-01-23', '2026-01-08'),
(31, 38, 30, '2026-01-08', '2026-01-23', '2026-01-08'),
(32, 41, 45, '2025-12-01', '2025-12-16', NULL),
(34, 40, 46, '2025-12-02', '2025-12-17', NULL),
(36, 42, 47, '2025-12-03', '2025-12-19', NULL),
(37, 43, 50, '2025-11-10', '2025-11-25', '2026-01-14'),
(38, 44, 49, '2025-11-02', '2025-11-17', '2026-01-09'),
(39, 45, 51, '2025-11-25', '2025-12-10', NULL),
(40, 73, 63, '2026-01-09', '2026-01-24', '2026-01-14'),
(41, 70, 63, '2026-01-14', '2026-01-29', NULL),
(42, 47, 41, '2026-01-14', '2026-01-29', NULL);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `Category`
--

CREATE TABLE `Category` (
  `CategoryId` int NOT NULL COMMENT 'Kategorinin benzersiz kimliği',
  `CategoryName` varchar(100) DEFAULT NULL COMMENT 'Kategorinin adı(roman)'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `departman`
--

CREATE TABLE `departman` (
  `departmanID` int NOT NULL,
  `departmanAdı` varchar(300) NOT NULL,
  `departmanİletisim` varchar(11) NOT NULL,
  `departmanKatı` int DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `departman`
--

INSERT INTO `departman` (`departmanID`, `departmanAdı`, `departmanİletisim`, `departmanKatı`) VALUES
(1, 'ybs', '0544444444', 1),
(3, 'muhasebe', '123', 3),
(9, 'üretim', '254', 4);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `Members`
--

CREATE TABLE `Members` (
  `MemberId` int NOT NULL COMMENT 'Kategorinin benzersiz kimliği',
  `FirstName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT 'Üye adı',
  `LastName` varchar(100) DEFAULT NULL COMMENT 'Üye soyadı',
  `Phone` varchar(14) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Üye telefonu',
  `Email` varchar(100) NOT NULL COMMENT 'Üye epostası(Tekil olmalı)',
  `RegisterDate` date NOT NULL COMMENT 'Üyelik tarihi',
  `Status` varchar(20) NOT NULL DEFAULT 'Aktif',
  `UpdateDate` datetime DEFAULT NULL,
  `BirthDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `Members`
--

INSERT INTO `Members` (`MemberId`, `FirstName`, `LastName`, `Phone`, `Email`, `RegisterDate`, `Status`, `UpdateDate`, `BirthDate`) VALUES
(19, 'Ahmet', 'Demir', '0532 741 23 89', 'ahmet.demir@gamil.com', '2026-01-07', 'Aktif', NULL, '1991-03-14'),
(20, 'Elif', 'Demir', '0541 603 45 11', 'elif.kaya@gamil.com', '2026-01-07', 'Aktif', NULL, '2001-07-22'),
(21, 'Mehmet', 'Yılmaz', '0553 912 66 40', 'mehmet.yilmaz@gmail.com', '2026-01-07', 'Aktif', NULL, '1995-11-09'),
(22, 'Zeynep', 'Aydın', '0506 334 78 92', 'zeynep.aydin@gmail.com', '2026-01-07', 'Aktif', NULL, '2003-01-18'),
(23, 'Burak', 'Şahin', '0545 281 54 60', 'burak.sahin@gamil.com', '2026-01-07', 'Aktif', NULL, '1997-06-30'),
(24, 'Merve', 'Çelik', '0539 774 19 88', 'merve.celik@gmail.com', '2026-01-07', 'Aktif', NULL, '2000-09-05'),
(25, 'Can', 'Arslan', '0552 608 41 73', 'can.arslan@gmail.com', '2026-01-07', 'Aktif', NULL, '1994-12-27'),
(26, 'Ayşe', 'Koç', '0507 962 35 14', 'ayse.koc@gmail.com', '2026-01-07', 'Aktif', NULL, '2002-04-11'),
(27, 'Emre', 'Polat', '0542 187 90 56', 'emre.polat@gmail.com', '2026-01-07', 'Aktif', NULL, '1996-08-08'),
(28, 'Selin', 'Yıldız', '0533 409 62 81', 'selin.yildiz@gamil.com', '2026-01-07', 'Aktif', NULL, '2004-02-19'),
(29, 'Kaan', 'Güneş', '0551 720 48 69', 'kaan.gunes@gmail.com', '2026-01-07', 'Aktif', NULL, '1999-05-03'),
(30, 'Derya', 'Öztürk', '0544 831 05 22', 'derya.ozturk@gmail.com', '2026-01-07', 'Aktif', NULL, '1997-10-26'),
(31, 'Oğuz', 'Karaca', '0536 998 71 44', 'oguz.karaca@gmail.com', '2026-01-07', 'Aktif', NULL, '1993-01-15'),
(32, 'Buse', 'Aksoy', '0505 664 38 95', 'buse.aksoy@gmail.com', '2026-01-07', 'Aktif', NULL, '2001-07-07'),
(33, 'Hakan', 'Erdem', '0546 513 26 70', 'hakan.erdem@gmail.com', '2026-01-07', 'Aktif', NULL, '1990-10-21'),
(34, 'Ceren', 'Toprak', '0531 442 80 33', 'ceren.toprak@gmail.com', '2026-01-07', 'Aktif', NULL, '2003-12-11'),
(35, 'Serkan', 'Balcı', '0554 176 94 58', 'serkan.balci@gmail.com', '2026-01-07', 'Aktif', NULL, '1996-03-29'),
(36, 'İrem', 'Korkmaz', '0508 305 61 09', 'irem.korkmaz@gmail.com', '2026-01-07', 'Aktif', NULL, '2002-06-17'),
(37, 'Furkan', 'Taş', '0540 889 47 66', 'furkan.tas@gmail.com', '2026-01-07', 'Aktif', NULL, '1998-04-01'),
(38, 'Melisa', 'Uslu', '0537 552 13 90', 'melisa.uslu@gmail.com', '2026-01-07', 'Aktif', NULL, '2000-08-24'),
(41, 'Merve', 'Yılmaz', '0542 586 54 75', 'merve18.05.2006@gmail.com', '2026-01-09', 'Aktif', NULL, '2006-05-18'),
(42, 'Zeynep', 'Yılmaz', '0548 569 48 21', 'zeynep.yilmaz@gmail.com', '2026-01-09', 'Aktif', NULL, '2012-05-06'),
(43, 'Kadir', 'Yılmaz', '0548 503 66 42', 'kadir.yilmaz@gmail.com', '2026-01-09', 'Aktif', NULL, '2007-09-28'),
(45, 'Ali', 'Demirtaş', '0534 218 47 90', 'ali.demirtas@gmail.com', '2025-12-01', 'Aktif', NULL, '1999-12-01'),
(46, 'Zinnet', 'Yılmaz', '0541 763 29 84', 'zinnet.yilmaz@gmail.com', '2025-12-02', 'Aktif', NULL, '1990-01-12'),
(47, 'Emre', 'Yavuz', '0552 904 61 37', 'emre.yavuz@gmail.com', '2025-12-03', 'Aktif', NULL, '2001-01-14'),
(48, 'Aybüke', 'Karahan', '0507 332 58 66', 'aybuke.karahan@gmail.com', '2025-12-02', 'Aktif', NULL, '2001-02-24'),
(49, 'Mert', 'Özcan', '0545 689 13 42', 'mert.ozcan@gmail.com', '2025-11-02', 'Aktif', NULL, '2005-06-14'),
(50, 'İlayda', 'Sarı', '0538 771 05 19', 'esra.sari@gmail.com', '2025-11-10', 'Aktif', NULL, '2007-09-25'),
(51, 'Nisa', 'Yıldırım', '0531 915 60 88', 'nisa.yildirim@gmail.com', '2025-11-25', 'Aktif', NULL, '2006-02-08'),
(53, 'Onur', 'Bayram', '0543 208 74 55', 'onur.bayram@gmail.com', '2025-10-04', 'Aktif', NULL, '2005-10-26'),
(55, 'Rukiye', 'Şimşek', '0538 771 05 19', 'rukiye.simsek@gmail.com', '2025-10-14', 'Aktif', NULL, '2008-05-26'),
(60, 'Şilan', 'Bilgin', '0548 962 74 32', 'silan.bilgin@gmail.com', '2025-10-04', 'Aktif', NULL, '2006-04-26'),
(61, 'Şilan', 'Bilgin', '0548 659 15 47', 'silan.bilgin', '2025-10-04', 'Pasif', '2026-01-09 18:58:11', '2005-10-05'),
(62, 'Şilan', 'Bilgin', '0548 659 15 47', 'silan.bilginqgmail.com', '2025-10-04', 'Pasif', '2026-01-09 18:58:05', '2005-10-05'),
(63, 'Sümeyra Esengül', 'Yılmaz', '0548 967 45 52', 'sümeyra@gmail.com', '2026-01-09', 'Aktif', NULL, '2015-10-20');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `personel`
--

CREATE TABLE `personel` (
  `ıd` int NOT NULL,
  `ad` text NOT NULL,
  `eposta` text NOT NULL,
  `did` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `personel`
--

INSERT INTO `personel` (`ıd`, `ad`, `eposta`, `did`) VALUES
(1, 'zeynep', 'zeynep@gmail.com', 1),
(2, 'merve', 'merve@gmail.com', 1),
(3, 'sümeyra', 'sümeyra@gmail.com', 3),
(6, 'kadir', 'kadir@gmail.com', 3);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `User`
--

CREATE TABLE `User` (
  `UserId` int NOT NULL COMMENT 'Kullanıcının benzersiz kimliği',
  `Username` varchar(50) NOT NULL COMMENT 'Sisteme giriş için kullanıcı adı',
  `PasswordHash` varchar(255) NOT NULL COMMENT 'Şifrenin güvenli hash değeri',
  `RoleId` int NOT NULL COMMENT 'kullanıcının rolü(1:yönetici;2:görevli,3:üye)',
  `MemberId` int DEFAULT NULL COMMENT 'eğer kullanıcı bir üyeyi temsil ediyorsa'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `User`
--

INSERT INTO `User` (`UserId`, `Username`, `PasswordHash`, `RoleId`, `MemberId`) VALUES
(1, 'yonetici', '1234', 1, 0),
(2, 'personel', '12345', 2, NULL),
(3, 'uye', '12345', 3, 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `Users`
--

CREATE TABLE `Users` (
  `Id` int NOT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Role` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Tablo döküm verisi `Users`
--

INSERT INTO `Users` (`Id`, `Username`, `Password`, `Role`) VALUES
(1, 'admin', '1234', 'Admin'),
(2, 'ali', '123', 'Personel'),
(3, 'ayse', '12345', 'Ogrenci');

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `B`
--
ALTER TABLE `B`
  ADD PRIMARY KEY (`BookId`,`Title`),
  ADD UNIQUE KEY `ISBN` (`ISBN`);

--
-- Tablo için indeksler `Books`
--
ALTER TABLE `Books`
  ADD PRIMARY KEY (`BookId`);

--
-- Tablo için indeksler `Borrows`
--
ALTER TABLE `Borrows`
  ADD PRIMARY KEY (`BorrowId`);

--
-- Tablo için indeksler `Category`
--
ALTER TABLE `Category`
  ADD PRIMARY KEY (`CategoryId`),
  ADD UNIQUE KEY `CategoryName` (`CategoryName`);

--
-- Tablo için indeksler `departman`
--
ALTER TABLE `departman`
  ADD PRIMARY KEY (`departmanID`);

--
-- Tablo için indeksler `Members`
--
ALTER TABLE `Members`
  ADD PRIMARY KEY (`MemberId`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- Tablo için indeksler `personel`
--
ALTER TABLE `personel`
  ADD PRIMARY KEY (`ıd`);

--
-- Tablo için indeksler `User`
--
ALTER TABLE `User`
  ADD PRIMARY KEY (`UserId`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Tablo için indeksler `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `B`
--
ALTER TABLE `B`
  MODIFY `BookId` int NOT NULL AUTO_INCREMENT COMMENT 'Kategorinin benzersiz kimliği';

--
-- Tablo için AUTO_INCREMENT değeri `Books`
--
ALTER TABLE `Books`
  MODIFY `BookId` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=80;

--
-- Tablo için AUTO_INCREMENT değeri `Borrows`
--
ALTER TABLE `Borrows`
  MODIFY `BorrowId` int NOT NULL AUTO_INCREMENT COMMENT 'ödünç işleminin benzersiz kimliği', AUTO_INCREMENT=43;

--
-- Tablo için AUTO_INCREMENT değeri `Category`
--
ALTER TABLE `Category`
  MODIFY `CategoryId` int NOT NULL AUTO_INCREMENT COMMENT 'Kategorinin benzersiz kimliği';

--
-- Tablo için AUTO_INCREMENT değeri `departman`
--
ALTER TABLE `departman`
  MODIFY `departmanID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Tablo için AUTO_INCREMENT değeri `Members`
--
ALTER TABLE `Members`
  MODIFY `MemberId` int NOT NULL AUTO_INCREMENT COMMENT 'Kategorinin benzersiz kimliği', AUTO_INCREMENT=64;

--
-- Tablo için AUTO_INCREMENT değeri `personel`
--
ALTER TABLE `personel`
  MODIFY `ıd` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Tablo için AUTO_INCREMENT değeri `User`
--
ALTER TABLE `User`
  MODIFY `UserId` int NOT NULL AUTO_INCREMENT COMMENT 'Kullanıcının benzersiz kimliği', AUTO_INCREMENT=4;

--
-- Tablo için AUTO_INCREMENT değeri `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
