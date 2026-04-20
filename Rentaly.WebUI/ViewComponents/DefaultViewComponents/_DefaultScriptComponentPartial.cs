using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultScriptComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}