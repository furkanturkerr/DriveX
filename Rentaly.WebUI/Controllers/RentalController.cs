using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;

namespace RentalyNew.Controllers;

public class RentalController : Controller
{
    private readonly IRentalService _rentalService;
    private readonly IBranchService _branchService;
    private readonly ICarService    _carService;

    public RentalController(IRentalService rentalService, IBranchService branchService, ICarService carService)
    {
        _rentalService = rentalService;
        _branchService = branchService;
        _carService    = carService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int carId)
    {
        var cars     = await _carService.TCarsWithCategoryAsync();
        var car      = cars.FirstOrDefault(x => x.CarId == carId);

        if (car == null)
            return RedirectToAction("CarList", "Car");

        ViewBag.Car      = car;
        ViewBag.Branches = await _branchService.TGetListAsync();

        return View(new CreateRentalDto { CarId = carId });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRentalDto dto)
    {
        if (!ModelState.IsValid)
        {
            var cars = await _carService.TCarsWithCategoryAsync();
            ViewBag.Car      = cars.FirstOrDefault(x => x.CarId == dto.CarId);
            ViewBag.Branches = await _branchService.TGetListAsync();
            return View(dto);
        }

        await _rentalService.TInsertAsync(dto);
        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }
}