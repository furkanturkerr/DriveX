using Rentaly.DtoLayer.CarDtos;

namespace Rentaly.BusinessLayer.Abstract;

public interface ICarService : IGenericService<ResultCarDto, CreateCarDto, UpdateCarDto>
{
    Task<List<ResultCarWithCategory>> TCarsWithCategoryAsync();
    Task<List<ResultCarWithCategory>> TCarsFilterAsync(CarFilterDto carFilterDto);
}