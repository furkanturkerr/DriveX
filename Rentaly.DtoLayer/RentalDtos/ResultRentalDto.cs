using Rentaly.DtoLayer.RentalDtos;

namespace Rentaly.DtoLayer.RentalDtos;

public class ResultRentalDto
{
    public int    RentalId      { get; set; }
    public string FirstName     { get; set; }
    public string LastName      { get; set; }
    public string Phone         { get; set; }
    public string Email         { get; set; }

    // Araç
    public string BrandName     { get; set; }
    public string ImageUrl      { get; set; }

    // Şube
    public string PickupBranchName  { get; set; }
    public string DropoffBranchName { get; set; }

    // Tarih
    public DateTime PickupDate  { get; set; }
    public DateTime DropoffDate { get; set; }

    // Fiyat
    public int     TotalDays    { get; set; }
    public decimal DailyPrice   { get; set; }
    public decimal TotalPrice   { get; set; }

    public string PlateNumber { get; set; }
    public string ModelName { get; set; }
    
    
    // Durum
    public string    Status     { get; set; }
    public DateTime  CreatedAt  { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string?   AdminNote  { get; set; }
}