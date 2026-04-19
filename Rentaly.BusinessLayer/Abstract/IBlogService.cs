using Rentaly.DtoLayer.BlogDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Abstract;

public interface IBlogService : IGenericService<ResultBlogDto, CreateBlogDto, UpdateBlogDto>
{
    Task<List<ResultBlogWithCategoryDto>> TGetBlogsWithCategoryAsync();
}