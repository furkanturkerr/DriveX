using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.Controllers;

public class DefaultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}