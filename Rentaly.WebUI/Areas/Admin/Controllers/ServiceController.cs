using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.ServiceValidations;
using Rentaly.DtoLayer.ServiceDtos;

namespace RentalyNew.Areas.Admin.Controllers;

[Area("Admin")]
public class ServiceController : Controller
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public async Task<IActionResult> ServiceList()
    {
        var values = await _serviceService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateService()
    {
        return View(new CreateServiceDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateService(CreateServiceDto dto)
    {
        var validator = new CreateServiceValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _serviceService.TInsertAsync(dto);
        return RedirectToAction("ServiceList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateService(int id)
    {
        var value = await _serviceService.TGetByIdAsync(id);
        return View(value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
    {
        var validator = new UpdateServiceValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(dto);
        }

        await _serviceService.TUpdateAsync(dto);
        return RedirectToAction("ServiceList");
    }

    public async Task<IActionResult> DeleteService(int id)
    {
        await _serviceService.TDeleteAsync(id);
        return RedirectToAction("ServiceList");
    }
}