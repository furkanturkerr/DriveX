using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
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

    // GET
    public async Task<IActionResult> ServiceList()
    {
        var values = await _serviceService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateService()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateService(CreateServiceDto dto)
    {
        await _serviceService.TInsertAsync(dto);
        return RedirectToAction("ServiceList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateService(int id)
    {
        var values = await _serviceService.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
    {
        await _serviceService.TUpdateAsync(dto);
        return RedirectToAction("ServiceList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteService(int id)
    {
        await _serviceService.TDeleteAsync(id);
        return RedirectToAction("ServiceList");
    }

}