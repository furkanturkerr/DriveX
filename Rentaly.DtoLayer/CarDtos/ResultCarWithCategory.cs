namespace Rentaly.DtoLayer.CarDtos;

public class ResultCarWithCategory
{
    public int     CarId        { get; set; }
    public string  PlateNumber  { get; set; }
    public string  VIN          { get; set; }
    public int     SeatCount    { get; set; }
    public int     LuggageCount { get; set; }
    public int     Year         { get; set; }
    public int     Kilometer    { get; set; }
    public decimal DailyPrice   { get; set; }
    public string  ImageUrl     { get; set; }
    public string  BrandName    { get; set; }
    public string  ModelName    { get; set; }
    public string  CategoryName { get; set; }
    public string  FuelType     { get; set; }
    public bool    IsAvailable  { get; set; }
    public bool    IsActive     { get; set; }
}