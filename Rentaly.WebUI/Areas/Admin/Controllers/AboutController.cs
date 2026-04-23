using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.AboutValidations;
using Rentaly.DtoLayer.AboutDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class AboutController : Controller
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    // GET
    public async Task<IActionResult> AboutList()
    {
        var values = await _aboutService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateAbout()
    {
        return View();
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        var validator = new CreateAboutValidator(); 
        var result = validator.Validate(createAboutDto);
        
        if (!result.IsValid)
        {
            foreach (var error in result.Errors) 
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage); 
            
            return View(createAboutDto);
        } 

        await _aboutService.TInsertAsync(createAboutDto);
        return RedirectToAction("AboutList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateAbout(int id)
    {
        var values = await _aboutService.TGetByIdAsync(id);
        return View(values);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        var validator = new UpdateAboutValidator(); 
        var result = validator.Validate(updateAboutDto);
        
        if (!result.IsValid)
        {
            foreach (var error in result.Errors) 
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage); 
            
            return View(updateAboutDto);
        } 

        await _aboutService.TUpdateAsync(updateAboutDto);
        return RedirectToAction("AboutList");
    }

    public async Task<IActionResult> DeleteAbout(int id)
    {
        await _aboutService.TDeleteAsync(id);
        return RedirectToAction("AboutList");
    }
}