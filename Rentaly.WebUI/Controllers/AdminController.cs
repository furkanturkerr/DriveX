using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}