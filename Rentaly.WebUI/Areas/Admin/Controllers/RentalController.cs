using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;

namespace RentalyNew.Areas.Admin.Controllers;

[Area("Admin")]
public class RentalController : Controller
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public async Task<IActionResult> RentalList()
    {
        var values = await _rentalService.GetRentalWithDetailsAsync();
        return View(values);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateRental(int id)
    {
        var value = await _rentalService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateRental(UpdateRentalDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _rentalService.TUpdateAsync(dto);
        return RedirectToAction(nameof(RentalList));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRental(int id)
    {
        await _rentalService.TDeleteAsync(id);
        return RedirectToAction(nameof(RentalList));
    }
}