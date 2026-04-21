using Rentaly.DtoLayer.CarCategoryDtos;

namespace Rentaly.BusinessLayer.Abstract;

public interface ICarCategoryService : IGenericService<ResultCarCategoryDto, CreateCarCategoryDto, UpdateCarCategoryDto>
{
    
}