using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;
using RentalyNew.Models;

namespace RentalyNew.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;
    private readonly IBrandService _brandService;
    private readonly ICarCategoryService _carCategoryService;
    private readonly IRentalService _rentalService;
    
    public CarController(ICarService carService, IBrandService brandService, ICarCategoryService carCategoryService, IRentalService rentalService)
    {
        _carService = carService;
        _brandService = brandService;
        _carCategoryService = carCategoryService;
        _rentalService = rentalService;
    }

    public async Task<IActionResult> CarList(CarFilterDto carFilterDto, int page = 1)
    {
        int pageSize = 6;

        var values = await _carService.TCarsFilterAsync(carFilterDto);

        // Tarih girilmişse çakışan rezervasyonları filtrele
        if (carFilterDto.PickupDate.HasValue && carFilterDto.DropoffDate.HasValue)
        {
            var unavailableCarIds = await _rentalService
                .TGetUnavailableCarIdsAsync(
                    carFilterDto.PickupDate.Value,
                    carFilterDto.DropoffDate.Value);

            values = values
                .Where(x => !unavailableCarIds.Contains(x.CarId))
                .ToList();
        }

        var totalCount = values.Count;
        var pagedCars  = values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var brands        = await _brandService.TGetListAsync();
        var carCategories = await _carCategoryService.TGetListAsync();

        ViewBag.brand        = new SelectList(brands, "BrandId", "BrandName");
        ViewBag.carCategory  = new SelectList(carCategories, "CarCategoryId", "CarCategoryName");
        ViewBag.PickupDate   = carFilterDto.PickupDate?.ToString("yyyy-MM-dd");
        ViewBag.DropoffDate  = carFilterDto.DropoffDate?.ToString("yyyy-MM-dd");

        var model = new CarlistModel
        {
            Cars        = pagedCars,
            Filter      = carFilterDto,
            CurrentPage = page,
            PageSize    = pageSize,
            TotalCount  = totalCount,
            TotalPages  = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return View(model);
    }
    
}