using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.AdminViewComponents;

public class _AdminHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}