using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfBlogDal : GenericRepository<Blog>, IBlogDal
{
    private readonly RentalyContext _context;
    
    public EfBlogDal(RentalyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Blog>> GetBlogsWithCategoryAsync()
    {
        var values = await _context.Blogs.Include(b => b.Category).ToListAsync();
        return values;
    }
}