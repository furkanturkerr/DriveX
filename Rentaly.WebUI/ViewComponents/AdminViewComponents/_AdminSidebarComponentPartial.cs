using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.AdminViewComponents;

public class _AdminSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}