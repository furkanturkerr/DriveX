using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.ContactDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class ContactController : Controller
{
    private readonly IContactService _ContactService;

    public ContactController(IContactService ContactService)
    {
        _ContactService = ContactService;
    }

    // GET
    public async Task<IActionResult> ContactList()
    {
        var values = await _ContactService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateContact()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto dto)
    {
        await _ContactService.TInsertAsync(dto);
        return RedirectToAction("ContactList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateContact(int id)
    {
        var values = await _ContactService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateContact(UpdateContactDto dto)
    {
        await _ContactService.TUpdateAsync(dto);
        return RedirectToAction("ContactList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _ContactService.TDeleteAsync(id);
        return RedirectToAction("ContactList");
    }

}