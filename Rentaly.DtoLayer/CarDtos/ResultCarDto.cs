namespace Rentaly.DtoLayer.CarDtos;

public class ResultCarDto
{
    public int CarId { get; set; }
    public string PlateNumber { get; set; }
    public string VIN { get; set; } 
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int SeatCount { get; set; }
    public int LuggageCount { get; set; }
    public int CarCategoryId { get; set; }
    public int BranchId { get; set; }
    public int Year { get; set; }
    public int Kilometer { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
    public string CarModelId { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public string FuelType { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsActive { get; set; }
}