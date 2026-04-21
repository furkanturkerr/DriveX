using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarCategoryDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class CarCategoryController : Controller
{
    private readonly ICarCategoryService _carCategoryService;

    public CarCategoryController(ICarCategoryService carCategoryService)
    {
        _carCategoryService = carCategoryService;
    }


    // GET
    public async Task<IActionResult> CarCategoryList()
    {
        var values = await _carCategoryService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateCarCategory()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateCarCategory(CreateCarCategoryDto dto)
    {
        await _carCategoryService.TInsertAsync(dto);
        return RedirectToAction("CarCategoryList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateCarCategory(int id)
    {
        var values = await _carCategoryService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateCarCategory(UpdateCarCategoryDto dto)
    {
        await _carCategoryService.TUpdateAsync(dto);
        return RedirectToAction("CarCategoryList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCarCategory(int id)
    {
        await _carCategoryService.TDeleteAsync(id);
        return RedirectToAction("CarCategoryList");
    }
}