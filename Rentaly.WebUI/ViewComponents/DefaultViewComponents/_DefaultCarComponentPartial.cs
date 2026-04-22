using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultCarComponentPartial : ViewComponent
{
    private readonly ICarService _carService;

    public _DefaultCarComponentPartial(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _carService.TCarsWithCategoryAsync();
        return View(values);
    }
}