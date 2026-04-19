using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BlogDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;

    public BlogController(IBlogService blogService, ICategoryService categoryService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
    }

    // GET
    public async Task<IActionResult> BlogList()
    {
        var values = await _blogService.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public async Task<IActionResult> CreateBlog()
    {
        var categories = await _categoryService.TGetListAsync();

        ViewBag.category = new SelectList(
            categories, 
            "CategoryId", 
            "CategoryName"
        );

        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateBlog(CreateBlogDto dto)
    {
        dto.DateCreated = DateTime.Now;
        await _blogService.TInsertAsync(dto);
        return RedirectToAction("BlogList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateBlog(int id)
    {
        var categories = await _categoryService.TGetListAsync();
        var values = await _blogService.TGetByIdAsync(id);

        ViewBag.category = new SelectList(
            categories,
            "CategoryId",
            "CategoryName",
            values.CategoryId
        );
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateBlog(UpdateBlogDto dto)
    {
        await _blogService.TUpdateAsync(dto);
        return RedirectToAction("BlogList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        await _blogService.TDeleteAsync(id);
        return RedirectToAction("BlogList");
    }

}