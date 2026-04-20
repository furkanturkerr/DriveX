using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultHeroComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}