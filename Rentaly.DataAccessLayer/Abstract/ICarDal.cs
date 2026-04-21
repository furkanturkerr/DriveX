using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Abstract;

public interface ICarDal : IGenericDal<Car>
{
    Task<List<Car>> CarsWithCategoryAsync();
}