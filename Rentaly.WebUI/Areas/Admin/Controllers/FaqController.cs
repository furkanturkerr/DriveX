using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.FaqValidations;
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

    public async Task<IActionResult> FaqList()
    {
        var values = await _faqService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateFaq()
    {
        return View(new CreateFaqDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFaq(CreateFaqDto dto)
    {
        var validator = new CreateFaqValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _faqService.TInsertAsync(dto);
        return RedirectToAction("FaqList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateFaq(int id)
    {
        var value = await _faqService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateFaq(UpdateFaqDto dto)
    {
        var validator = new UpdateFaqValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _faqService.TUpdateAsync(dto);
        return RedirectToAction("FaqList");
    }

    public async Task<IActionResult> DeleteFaq(int id)
    {
        await _faqService.TDeleteAsync(id);
        return RedirectToAction("FaqList");
    }
}