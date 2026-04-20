using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultContactComponentPartial : ViewComponent
{
    private readonly IContactService _contactService;

    public _DefaultContactComponentPartial(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _contactService.TGetListAsync();
        return View(values);
    }
}