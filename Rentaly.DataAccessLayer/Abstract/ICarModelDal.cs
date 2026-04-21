using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Abstract;

public interface ICarModelDal : IGenericDal<CarModel>
{
    Task<List<CarModel>> GetCarModelsWithBrandAsync();
}