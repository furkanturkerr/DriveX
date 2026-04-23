using Microsoft.AspNetCore.Mvc;

namespace RentalyNew.Controllers;

public class ErrorController : Controller
{
    // GET
    [Route("Error/404")]
    public IActionResult Page404()
    {
        return View();
    }
}