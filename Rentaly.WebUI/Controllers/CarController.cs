using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;

namespace RentalyNew.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> CarList(
        string? brand,
        string? category,
        string? fuel,
        decimal? minPrice,
        decimal? maxPrice,
        int page = 1)
    {
        var filter = new CarFilterRequestDto
        {
            Brand    = brand,
            Category = category,
            Fuel     = fuel,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Page     = page
        };

        var result = await _carService.TGetPagedCarListAsync(filter);
        return View(result);
    }
}