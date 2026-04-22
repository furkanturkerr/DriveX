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
    
    public CarController(ICarService carService, IBrandService brandService, ICarCategoryService carCategoryService)
    {
        _carService = carService;
        _brandService = brandService;
        _carCategoryService = carCategoryService;
    }

    public async Task<IActionResult> CarList(CarFilterDto carFilterDto, int page = 1)
    {
        int pageSize = 6;

        var values = await _carService.TCarsFilterAsync(carFilterDto);

        var totalCount = values.Count();

        var pagedCars = values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var brands = await _brandService.TGetListAsync();
        var carCategories = await _carCategoryService.TGetListAsync();

        ViewBag.brand = new SelectList(brands, "BrandId", "BrandName");
        ViewBag.carCategory = new SelectList(carCategories, "CarCategoryId", "CarCategoryName");

        var model = new CarlistModel
        {
            Cars = pagedCars,
            Filter = carFilterDto,
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };

        return View(model);
    }
}