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
    public async Task<IActionResult> Create(int carId, DateTime? pickupDate, DateTime? dropoffDate)
    {
        var cars = await _carService.TCarsWithCategoryAsync();
        var car  = cars.FirstOrDefault(x => x.CarId == carId);

        if (car == null)
            return RedirectToAction("CarList", "Car");

        // Tarih bazlı müsaitlik kontrolü
        if (pickupDate.HasValue && dropoffDate.HasValue)
        {
            var unavailable = await _rentalService
                .TGetUnavailableCarIdsAsync(pickupDate.Value, dropoffDate.Value);

            if (unavailable.Contains(carId))
                return RedirectToAction("CarList", "Car");
        }

        ViewBag.Car      = car;
        ViewBag.Branches = await _branchService.TGetListAsync();

        var dto = new CreateRentalDto
        {
            CarId       = carId,
            PickupDate  = pickupDate ?? DateTime.Today,
            DropoffDate = dropoffDate ?? DateTime.Today.AddDays(1)
        };

        return View(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBookedDates(int carId)
    {
        var ranges = await _rentalService.TGetBookedDatesAsync(carId);
        return Json(ranges);
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