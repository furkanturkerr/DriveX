using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.FooterValidations;
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

    public async Task<IActionResult> FooterList()
    {
        var values = await _footerService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateFooter()
    {
        return View(new CreateFooterDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFooter(CreateFooterDto dto)
    {
        var validator = new CreateFooterValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _footerService.TInsertAsync(dto);
        return RedirectToAction("FooterList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateFooter(int id)
    {
        var value = await _footerService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateFooter(UpdateFooterDto dto)
    {
        var validator = new UpdateFooterValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _footerService.TUpdateAsync(dto);
        return RedirectToAction("FooterList");
    }

    public async Task<IActionResult> DeleteFooter(int id)
    {
        await _footerService.TDeleteAsync(id);
        return RedirectToAction("FooterList");
    }
}