namespace Rentaly.DtoLayer.CarDtos;

public class CarListPagedResultDto
{
    public List<ResultCarWithCategory> Cars { get; set; } = new();

    public List<string> AllBrands     { get; set; } = new();
    public List<string> AllCategories { get; set; } = new();
    public List<string> AllFuelTypes  { get; set; } = new();

    public string?  BrandFilter    { get; set; }
    public string?  CategoryFilter { get; set; }
    public string?  FuelFilter     { get; set; }
    public decimal? MinPrice       { get; set; }
    public decimal? MaxPrice       { get; set; }

    public int CurrentPage { get; set; }
    public int TotalPages  { get; set; }
    public int TotalItems  { get; set; }
}