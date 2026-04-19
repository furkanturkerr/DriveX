using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CategoryDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _CategoryCategory;

    public CategoryController(ICategoryService CategoryCategory)
    {
        _CategoryCategory = CategoryCategory;
    }


    // GET
    public async Task<IActionResult> CategoryList()
    {
        var values = await _CategoryCategory.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
    {
        await _CategoryCategory.TInsertAsync(dto);
        return RedirectToAction("CategoryList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        var values = await _CategoryCategory.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        await _CategoryCategory.TUpdateAsync(dto);
        return RedirectToAction("CategoryList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _CategoryCategory.TDeleteAsync(id);
        return RedirectToAction("CategoryList");
    }

}