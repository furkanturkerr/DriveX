using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.Controllers;

public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}