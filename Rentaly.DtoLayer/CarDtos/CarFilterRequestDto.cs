namespace Rentaly.DtoLayer.CarDtos;


public class CarFilterRequestDto
{
    public string?  Brand    { get; set; }
    public string?  Category { get; set; }
    public string?  Fuel     { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int      Page     { get; set; } = 1;
    public int      PageSize { get; set; } = 6;
}