using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Abstract;

public interface ICarDal : IGenericDal<Car>
{
    Task<List<Car>> CarsWithCategoryAsync();
    Task<List<Car>> CarsWithStatusAsync();
    Task<List<Car>> GetFilteredCars(int? brandId, int? carCategoryId, string? fuel, decimal? minPrice, decimal? maxPrice);
    Task<Car> GetCarWithDetailsAsync(int id);
}