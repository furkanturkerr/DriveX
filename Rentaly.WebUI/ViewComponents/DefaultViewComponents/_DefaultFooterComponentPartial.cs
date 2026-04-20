using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultFooterComponentPartial : ViewComponent
{
    private readonly IFooterService _footerService;

    public _DefaultFooterComponentPartial(IFooterService footerService)
    {
        _footerService = footerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _footerService.TGetListAsync();
        return View(values);
    }
}