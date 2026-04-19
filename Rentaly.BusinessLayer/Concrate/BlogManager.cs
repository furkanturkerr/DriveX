using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.BlogDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class BlogManager : IBlogService
{
    private readonly IBlogDal _blogDal;
    private readonly IMapper _mapper;

    public BlogManager(IBlogDal blogDal, IMapper mapper)
    {
        _blogDal = blogDal;
        _mapper = mapper;
    }

    public async Task<List<ResultBlogDto>> TGetListAsync()
    {
        var result = await _blogDal.GetListAsync();
        return _mapper.Map<List<ResultBlogDto>>(result);
    }

    public async Task<UpdateBlogDto> TGetByIdAsync(int id)
    {
        var result = await _blogDal.GetByIdAsync(id);
        return _mapper.Map<UpdateBlogDto>(result);
    }

    public async Task TInsertAsync(CreateBlogDto dto)
    {
        var blog = _mapper.Map<Blog>(dto);
        await _blogDal.InsertAsync(blog);
    }

    public async Task TUpdateAsync(UpdateBlogDto dto)
    {
        var blog = _mapper.Map<Blog>(dto);
        await _blogDal.UpdateAsync(blog);
    }

    public async Task TDeleteAsync(int id)
    {
        await _blogDal.DeleteAsync(id);
    }

    public async Task<List<ResultBlogWithCategoryDto>> TGetBlogsWithCategoryAsync()
    {
        var values = await _blogDal.GetBlogsWithCategoryAsync();
        return _mapper.Map<List<ResultBlogWithCategoryDto>>(values);
    }
}