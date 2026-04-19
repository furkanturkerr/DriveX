using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Abstract;

public interface ICategoryService : IGenericService<ResultCategoryDto, CreateCategoryDto, UpdateCategoryDto>
{
    
}