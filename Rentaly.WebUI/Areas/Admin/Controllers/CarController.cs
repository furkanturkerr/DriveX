using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;

namespace RentalyNew.Areas.Admin.Controllers;

[Area("Admin")]
public class CarController : Controller
{
    private readonly ICarService _carService;
    private readonly IBrandService _brandService;
    private readonly ICarModelService _carModelService;
    private readonly ICarCategoryService _carCategoryService;
    private readonly IBranchService _branchService;

    public CarController(
        ICarService carService,
        ICarCategoryService carCategoryService,
        ICarModelService carModelService,
        IBrandService brandService,
        IBranchService branchService)
    {
        _carService = carService;
        _carCategoryService = carCategoryService;
        _carModelService = carModelService;
        _brandService = brandService;
        _branchService = branchService;
    }

    public async Task<IActionResult> CarList(int page = 1, string search = "",
        string fuelType = "", string status = "")
    {
        var all = await _carService.TCarsWithCategoryAsync();

        if (!string.IsNullOrEmpty(search))
            all = all.Where(x =>
                x.PlateNumber.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.BrandName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.VIN.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrEmpty(fuelType))
            all = all.Where(x => x.FuelType == fuelType).ToList();

        if (status == "available") all = all.Where(x => x.IsAvailable).ToList();
        if (status == "rented")    all = all.Where(x => !x.IsAvailable).ToList();
        if (status == "active")    all = all.Where(x => x.IsActive).ToList();
        if (status == "passive")   all = all.Where(x => !x.IsActive).ToList();

        int pageSize = 10;
        int totalPage = (int)Math.Ceiling(all.Count / (double)pageSize);

        ViewBag.TotalPage   = totalPage;
        ViewBag.CurrentPage = page;
        ViewBag.Search      = search;
        ViewBag.FuelType    = fuelType;
        ViewBag.Status      = status;

        return View(all.Skip((page - 1) * pageSize).Take(pageSize).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> CreateCar()
    {
        await LoadCarDropdownsAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCar(CreateCarDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadCarDropdownsAsync(dto.BrandId, dto.CarModelId, dto.CarCategoryId, dto.BranchId);
            return View(dto);
        }

        await _carService.TInsertAsync(dto);
        return RedirectToAction("CarList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCar(int id)
    {
        var values = await _carService.TGetByIdAsync(id);

        await LoadCarDropdownsAsync(
            values.BrandId,
            values.CarModelId,
            values.CarCategoryId,
            values.BranchId);

        return View(values);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCar(UpdateCarDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadCarDropdownsAsync(dto.BrandId, dto.CarModelId, dto.CarCategoryId, dto.BranchId);
            return View(dto);
        }

        await _carService.TUpdateAsync(dto);
        return RedirectToAction("CarList");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCar(int id)
    {
        await _carService.TDeleteAsync(id);
        return RedirectToAction("CarList");
    }

    [HttpGet]
    public async Task<JsonResult> GetModelsByBrand(int brandId)
    {
        var models = await _carModelService.GetCarModelsWithBrandAsync();

        var filteredModels = models
            .Where(x => x.BrandId == brandId)
            .Select(x => new
            {
                id = x.CarModelId,
                text = x.ModelName
            })
            .ToList();

        return Json(filteredModels);
    }

    private async Task LoadCarDropdownsAsync(
        int? selectedBrandId = null,
        int? selectedModelId = null,
        int? selectedCategoryId = null,
        int? selectedBranchId = null)
    {
        var brands = await _brandService.TGetListAsync();
        var categories = await _carCategoryService.TGetListAsync();
        var branches = await _branchService.TGetListAsync();

        ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName", selectedBrandId);
        ViewBag.CarCategories = new SelectList(categories, "CarCategoryId", "CarCategoryName", selectedCategoryId);
        ViewBag.Branches = new SelectList(branches, "BranchId", "BranchName", selectedBranchId);

        if (selectedBrandId.HasValue)
        {
            var models = await _carModelService.GetCarModelsWithBrandAsync();

            var filteredModels = models
                .Where(x => x.BrandId == selectedBrandId.Value)
                .ToList();

            ViewBag.Models = new SelectList(filteredModels, "CarModelId", "ModelName", selectedModelId);
        }
        else
        {
            ViewBag.Models = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
        }
    }
}