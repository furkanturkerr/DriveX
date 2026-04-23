<h1 align="center">DriveX</h1>

<p align="center">
  ASP.NET Core ile geliştirilmiş, yapay zeka destekli modern araç kiralama platformu.
</p>

---

<p align="center">
  <img src="https://img.shields.io/badge/.NET-ASP.NET_Core-blueviolet?style=for-the-badge" />
  <img src="https://img.shields.io/badge/EntityFramework-Core-green?style=for-the-badge" />
  <img src="https://img.shields.io/badge/SQL-Server-darkblue?style=for-the-badge" />
  <img src="https://img.shields.io/badge/FluentValidation-Validation-orange?style=for-the-badge" />
  <img src="https://img.shields.io/badge/AutoMapper-Mapping-red?style=for-the-badge" />
  <img src="https://img.shields.io/badge/MailKit-E--Mail-blue?style=for-the-badge" />
  <img src="https://img.shields.io/badge/OpenAI-AI-black?style=for-the-badge" />
</p>

---

## Proje Hakkında

Rentaly, kullanıcıların araç kiralama işlemlerini kolayca gerçekleştirebildiği ve admin panel üzerinden tüm sürecin yönetilebildiği modern bir web uygulamasıdır.

Proje, katmanlı mimari (N-Tier Architecture) ile geliştirilmiş olup performans, sürdürülebilirlik ve temiz kod prensipleri ön planda tutulmuştur. Yapay zeka entegrasyonu sayesinde iletişim süreçleri otomatik hale getirilmiştir.

---

## Kullanıcı Tarafı Özellikleri

- Dinamik anasayfa (Banner, About, Services, FAQ, Footer)
- Araç listeleme ve gelişmiş filtreleme
- Tarih ve şube bazlı rezervasyon sistemi
- Gün bazlı otomatik fiyat hesaplama
- Blog sistemi
- Responsive kullanıcı arayüzü
- Özel 404 sayfası

---

## Admin Panel Özellikleri

- Rezervasyon yönetimi (Onaylama, reddetme, durum güncelleme)
- Araç, marka, model ve kategori yönetimi
- Şube yönetimi
- Blog yönetimi
- Mesaj yönetimi (AI yanıt görüntüleme dahil)
- Dinamik anasayfa içerik yönetimi
- Sayfalama sistemi
- Otomatik mail gönderimi
- Admin panel arayüzü CloudAI kullanılarak tasarlanmış ve modern dark UI uygulanmıştır

---

## Yapay Zeka Entegrasyonu

- İletişim formunu dolduran kullanıcıya özel AI yanıt oluşturma
- Oluşturulan yanıtın otomatik olarak mail ile gönderilmesi

---

## Kullanılan Teknolojiler

### Backend
- ASP.NET Core 8
- Entity Framework Core
- SQL Server

### Katmanlar
- Entity Layer
- Data Access Layer
- Business Layer
- DTO Layer
- UI Layer

### Kütüphaneler
- AutoMapper (Entity ↔ DTO dönüşümleri)
- FluentValidation (veri doğrulama)
- MailKit (email gönderimi)

### Araçlar
- EF Core Tools
- Design Tools
- CodeGeneration.Design

---

## Mimari Yapı

Projede katmanlar arası bağımlılık minimum seviyede tutulmuştur:

- DataAccess yalnızca Entity ile çalışır
- DTO katmanı UI ve Business arasında veri taşır
- Business katmanı Entity ↔ DTO dönüşümünü yönetir
- UI sadece DTO kullanır

Bu yapı sayesinde sürdürülebilir ve temiz bir kod mimarisi elde edilmiştir.

---

## AutoMapper Kullanımı

Projede Entity ve DTO dönüşümleri AutoMapper ile yapılmaktadır.

- Kod tekrarını azaltır
- Katmanlar arası bağımlılığı düşürür
- Daha okunabilir ve yönetilebilir yapı sağlar

---

## Validasyon Yapısı

- FluentValidation kullanılmıştır
- Create ve Update işlemleri ayrı ayrı doğrulanır
- Hatalar controller üzerinden ModelState ile gösterilir
- ASP.NET default validation kullanılmamıştır

---

## Ekran Görüntüleri

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/1.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/2.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/3.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/4.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/5.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/6.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/7.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/8.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/10.png">


---

### Araç Listesi

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/a1.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/a2.png">

---

### Araç Kiralama

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/k1.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/k2.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/k3.png">

---

### Onay Maili

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/rezervasyonmail.png">

---

### İletişim Sayfası

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/contact.png">

---

### AI Mail Yanıtı

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/rezervasyonmail.png">

---

### 404 Sayfası

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/%C4%B0mages/e1.png">

---

## Admin Paneli

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/carlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/createcar.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/brandlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/carmodellist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/carcategory.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/branchlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/rezervationlsit.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/updaterezervation.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/messagelist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/messagedetail.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/bloglist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/createblog.png">

---

### Anasayfa Yönetimi

<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/aboutlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/servicelist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/bannerlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/testimoniallist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/faqlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/contactlist.png">
<img src="https://github.com/furkanturkerr/Rentaly/blob/main/Rentaly.WebUI/wwwroot/Admin/Images/footerlist.png">

---

## Sonuç

Rentaly, modern araç kiralama ihtiyaçlarına yönelik geliştirilmiş, yönetilebilir ve genişletilebilir bir platformdur. Katmanlı mimarisi ve yapay zeka entegrasyonu sayesinde sürdürülebilir ve profesyonel bir yapı sunar.
