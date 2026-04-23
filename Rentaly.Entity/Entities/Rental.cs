namespace Rentaly.Entity;

public class Rental
{
    public int RentalId { get; set; }

    // ===== ARAÇ =====
    public int CarId { get; set; }
    public Car Car { get; set; }

    // ===== KİRACİ BİLGİLERİ =====
    public string   FirstName      { get; set; }  // Ad
    public string   LastName       { get; set; }  // Soyad
    public string   IdentityNumber { get; set; }  // TC Kimlik No
    public string   Phone          { get; set; }  // Telefon
    public string   Email          { get; set; }  // E-posta
    public string   LicenseNumber  { get; set; }  // Ehliyet No
    public DateTime BirthDate      { get; set; }  // Doğum Tarihi

    // ===== ALIŞ / TESLİM ŞUBESİ =====
    public int    PickupBranchId  { get; set; }   // Alış Şubesi
    public Branch PickupBranch    { get; set; }

    public int    DropoffBranchId { get; set; }   // Teslim Şubesi
    public Branch DropoffBranch   { get; set; }

    // ===== TARİH & SAAT =====
    public DateTime PickupDate  { get; set; }     // Alış Tarihi & Saati
    public DateTime DropoffDate { get; set; }     // Teslim Tarihi & Saati

    // ===== FİYAT =====
    public int     TotalDays     { get; set; }    // Gün Sayısı (hesaplanan)
    public decimal DailyPrice    { get; set; }    // Günlük Fiyat (o andaki, değişmemesi için kopyalanır)
    public decimal TotalPrice    { get; set; }    // Toplam Tutar

    // ===== DURUM =====
    public RentalStatus Status { get; set; } = RentalStatus.Beklemede;

    // ===== TARİH KAYITLARI =====
    public DateTime  CreatedAt  { get; set; } = DateTime.Now;
    public DateTime? ApprovedAt { get; set; }
    public string?   AdminNote  { get; set; }
}

public enum RentalStatus
{
    Beklemede  = 0,  // Form gönderildi, admin bekliyor
    Onaylandi  = 1,  // Admin onayladı
    Reddedildi = 2,  // Admin reddetti
    Teslim     = 3,  // Araç teslim edildi
    Tamamlandi = 4,  // Araç iade edildi
    Iptal      = 5   // İptal edildi
}