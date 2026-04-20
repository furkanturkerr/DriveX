using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultSliderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}