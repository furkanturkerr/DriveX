using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.FaqDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class FaqController : Controller
{
    private readonly IFaqService _faqService;

    public FaqController(IFaqService faqService)
    {
        _faqService = faqService;
    }

    // GET
    public async Task<IActionResult> FaqList()
    {
        var values = await _faqService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateFaq()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateFaq(CreateFaqDto dto)
    {
        await _faqService.TInsertAsync(dto);
        return RedirectToAction("FaqList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateFaq(int id)
    {
        var values = await _faqService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateFaq(UpdateFaqDto dto)
    {
        await _faqService.TUpdateAsync(dto);
        return RedirectToAction("FaqList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFaq(int id)
    {
        await _faqService.TDeleteAsync(id);
        return RedirectToAction("FaqList");
    }

}