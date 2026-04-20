using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultBannerComponentPartial : ViewComponent
{
    private readonly IBannerService _bannerService;

    public _DefaultBannerComponentPartial(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _bannerService.TGetListAsync();
        return View(values);
    }
}