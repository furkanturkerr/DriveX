using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.BannerValidations;
using Rentaly.DtoLayer.BannerDtos;

namespace RentalyNew.Areas.Admin.Controllers;

[Area("Admin")]
public class BannerController : Controller
{
    private readonly IBannerService _bannerService;

    public BannerController(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    public async Task<IActionResult> BannerList()
    {
        var values = await _bannerService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateBanner()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBanner(CreateBannerDto dto)
    {
        var validator = new CreateBannerValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return View(dto);
        }

        await _bannerService.TInsertAsync(dto);
        return RedirectToAction("BannerList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBanner(int id)
    {
        var value = await _bannerService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateBanner(UpdateBannerDto dto)
    {
        var validator = new UpdateBannerValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return View(dto);
        }

        await _bannerService.TUpdateAsync(dto);
        return RedirectToAction("BannerList");
    }

    public async Task<IActionResult> DeleteBanner(int id)
    {
        await _bannerService.TDeleteAsync(id);
        return RedirectToAction("BannerList");
    }
}