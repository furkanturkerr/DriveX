using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultFaqComponentPartial : ViewComponent
{
    private readonly IFaqService _faqService;

    public _DefaultFaqComponentPartial(IFaqService faqService)
    {
        _faqService = faqService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _faqService.TGetListAsync();
        return View(values);
    }
}