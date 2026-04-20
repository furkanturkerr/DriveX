using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultAboutComponentPartial : ViewComponent
{
    private readonly IAboutService _aboutService;

    public _DefaultAboutComponentPartial(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _aboutService.TGetListAsync();
        return View(values);
    }
}