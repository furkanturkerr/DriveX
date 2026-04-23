namespace Rentaly.DtoLayer.CarDtos;

public class CarFilterDto
{
    public int? CarCategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? BrandId { get; set; }
    public string? BrandName { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? FuelType { get; set; }
    public DateTime? PickupDate   { get; set; }
    public DateTime? DropoffDate  { get; set; }
}