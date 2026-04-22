using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultHeroComponentPartial : ViewComponent
{
    private readonly IBrandService _brandService;

    public _DefaultHeroComponentPartial(IBrandService brandService)
    {
        _brandService = brandService;
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _brandService.TGetListAsync();

        ViewBag.brand = new SelectList(values, "BrandId", "BrandName");
        
        return View();
    }
}