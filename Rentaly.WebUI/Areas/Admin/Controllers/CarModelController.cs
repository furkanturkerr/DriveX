using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarModelDtos;

namespace RentalyNew.Areas.Admin.Controllers;

[Area("Admin")]
public class CarModelController : Controller
{
    private readonly ICarModelService _carModelService;
    private readonly IBrandService _brandService;

    public CarModelController(ICarModelService carModelService, IBrandService brandService)
    {
        _carModelService = carModelService;
        _brandService = brandService;
    }

    public async Task<IActionResult> CarModelList(int? brandId)
    {
        var allValues = await _carModelService.GetCarModelsWithBrandAsync();
        var brands = await _brandService.TGetListAsync();

        ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName", brandId);
        ViewBag.SelectedBrandId = brandId;

        if (brandId.HasValue)
        {
            allValues = allValues
                .Where(x => x.BrandId == brandId.Value)
                .ToList();
        }

        return View(allValues);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCarModel()
    {
        var values = await _brandService.TGetListAsync();
        ViewBag.brand = new SelectList(values, "BrandId", "BrandName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCarModel(CreateCarModelDto dto)
    {
        await _carModelService.TInsertAsync(dto);
        return RedirectToAction("CarModelList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCarModel(int id)
    {
        var values = await _carModelService.TGetByIdAsync(id);
        var brands = await _brandService.TGetListAsync();
        ViewBag.brand = new SelectList(brands, "BrandId", "BrandName", values.BrandId);
        return View(values);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCarModel(UpdateCarModelDto dto)
    {
        await _carModelService.TUpdateAsync(dto);
        return RedirectToAction("CarModelList");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCarModel(int id)
    {
        await _carModelService.TDeleteAsync(id);
        return RedirectToAction("CarModelList");
    }
}