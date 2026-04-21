using Rentaly.DtoLayer.CarModelDtos;

namespace Rentaly.BusinessLayer.Abstract;

public interface ICarModelService : IGenericService<ResultCarModelDto, CreateCarModelDto, UpdateCarModelDto>
{
    Task<List<ResultCarModelWithBrand>> GetCarModelsWithBrandAsync();
}