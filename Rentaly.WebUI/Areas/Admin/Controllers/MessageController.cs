using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<IActionResult> MessageList()
    {
        var values = await _messageService.TGetListAsync();
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> MessageDetail(int id)
    {
        var value = await _messageService.TGetByIdAsync(id);
        return View(value);
    }
}