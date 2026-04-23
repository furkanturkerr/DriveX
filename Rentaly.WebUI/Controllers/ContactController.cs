using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.MessageDtos;

namespace RentalyNew.Controllers;

public class ContactController : Controller
{
    private readonly IMessageService _messageService;

    public ContactController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateMessageDto dto)
    {
        await _messageService.TInsertAsync(dto);
        return RedirectToAction("Index");
    }
}