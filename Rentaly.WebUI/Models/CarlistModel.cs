using Rentaly.DtoLayer.CarDtos;

namespace RentalyNew.Models;

public class CarlistModel
{
    public CarFilterDto Filter { get; set; } = new();
    public List<ResultCarWithCategory> Cars { get; set; } = new();

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
}