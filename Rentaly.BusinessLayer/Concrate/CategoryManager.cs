using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;


    public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
    }

    public async Task<List<ResultCategoryDto>> TGetListAsync()
    {
        var values = await _categoryDal.GetListAsync();
        return _mapper.Map<List<ResultCategoryDto>>(values);
    }

    public async Task<UpdateCategoryDto> TGetByIdAsync(int id)
    {
        var values = await _categoryDal.GetByIdAsync(id);
        return _mapper.Map<UpdateCategoryDto>(values);
    }

    public async Task TInsertAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _categoryDal.InsertAsync(category);
    }

    public async Task TUpdateAsync(UpdateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _categoryDal.UpdateAsync(category);
    }

    public async Task TDeleteAsync(int id)
    {
        await _categoryDal.DeleteAsync(id);
    }
}