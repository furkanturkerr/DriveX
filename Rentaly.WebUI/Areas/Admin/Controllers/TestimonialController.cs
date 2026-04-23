using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.TestimonialValidations;
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

    public async Task<IActionResult> TestimonialList()
    {
        var values = await _testimonialService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateTestimonial()
    {
        return View(new CreateTestimonialDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto dto)
    {
        var validator = new CreateTestimonialValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _testimonialService.TInsertAsync(dto);
        return RedirectToAction("TestimonialList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTestimonial(int id)
    {
        var value = await _testimonialService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto dto)
    {
        var validator = new UpdateTestimonialValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _testimonialService.TUpdateAsync(dto);
        return RedirectToAction("TestimonialList");
    }

    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        await _testimonialService.TDeleteAsync(id);
        return RedirectToAction("TestimonialList");
    }
}