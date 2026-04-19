using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Abstract;

public interface IBlogDal : IGenericDal<Blog>
{
    Task<List<Blog>> GetBlogsWithCategoryAsync();
}