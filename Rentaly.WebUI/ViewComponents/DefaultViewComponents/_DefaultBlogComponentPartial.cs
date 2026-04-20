using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace RentalyNew.ViewComponents.DefaultViewComponents;

public class _DefaultBlogComponentPartial : ViewComponent
{
    private readonly IBlogService _blogService;

    public _DefaultBlogComponentPartial(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _blogService.TGetBlogsWithCategoryAsync();
        return View(values);
    }
}