using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultCarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}