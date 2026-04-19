using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BannerDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class BannerController : Controller
{
    private readonly IBannerService _BannerBanner;

    public BannerController(IBannerService bannerBanner)
    {
        _BannerBanner = bannerBanner;
    }


    // GET
    public async Task<IActionResult> BannerList()
    {
        var values = await _BannerBanner.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateBanner()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateBanner(CreateBannerDto dto)
    {
        await _BannerBanner.TInsertAsync(dto);
        return RedirectToAction("BannerList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateBanner(int id)
    {
        var values = await _BannerBanner.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateBanner(UpdateBannerDto dto)
    {
        await _BannerBanner.TUpdateAsync(dto);
        return RedirectToAction("BannerList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBanner(int id)
    {
        await _BannerBanner.TDeleteAsync(id);
        return RedirectToAction("BannerList");
    }

}