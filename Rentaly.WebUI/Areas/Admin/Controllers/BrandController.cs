using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BrandDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    // GET
    public async Task<IActionResult> BrandList()
    {
        var values = await _brandService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateBrand()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateBrand(CreateBrandDto dto)
    {
        await _brandService.TInsertAsync(dto);
        return RedirectToAction("BrandList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateBrand(int id)
    {
        var values = await _brandService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateBrand(UpdateBrandDto dto)
    {
        await _brandService.TUpdateAsync(dto);
        return RedirectToAction("BrandList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        await _brandService.TDeleteAsync(id);
        return RedirectToAction("BrandList");
    }
}