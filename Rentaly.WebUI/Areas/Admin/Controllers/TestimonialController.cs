using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.TestimonialDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class TestimonialController : Controller
{
    private readonly ITestimonialService _testimonialService;

    public TestimonialController(ITestimonialService testimonialService)
    {
        _testimonialService = testimonialService;
    }

    // GET
    public async Task<IActionResult> TestimonialList()
    {
        var values = await _testimonialService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateTestimonial()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto dto)
    {
        await _testimonialService.TInsertAsync(dto);
        return RedirectToAction("TestimonialList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateTestimonial(int id)
    {
        var values = await _testimonialService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto dto)
    {
        await _testimonialService.TUpdateAsync(dto);
        return RedirectToAction("TestimonialList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        await _testimonialService.TDeleteAsync(id);
        return RedirectToAction("TestimonialList");
    }

}