using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultServiceComponentPartial : ViewComponent
{
    private readonly IServiceService _serviceService;

    public _DefaultServiceComponentPartial(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _serviceService.TGetListAsync();
        return View(values);
    }
}