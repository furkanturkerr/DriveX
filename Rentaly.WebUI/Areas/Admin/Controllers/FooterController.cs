using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.FooterDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class FooterController : Controller
{
    private readonly IFooterService _footerService;

    public FooterController(IFooterService footerService)
    {
        _footerService = footerService;
    }

    // GET
    public async Task<IActionResult> FooterList()
    {
        var values = await _footerService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateFooter()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateFooter(CreateFooterDto dto)
    {
        await _footerService.TInsertAsync(dto);
        return RedirectToAction("FooterList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateFooter(int id)
    {
        var values = await _footerService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateFooter(UpdateFooterDto dto)
    {
        await _footerService.TUpdateAsync(dto);
        return RedirectToAction("FooterList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFooter(int id)
    {
        await _footerService.TDeleteAsync(id);
        return RedirectToAction("FooterList");
    }

}