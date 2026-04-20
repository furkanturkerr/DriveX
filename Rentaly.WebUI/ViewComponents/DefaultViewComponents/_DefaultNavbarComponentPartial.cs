using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultNavbarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}