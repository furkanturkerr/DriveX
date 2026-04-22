using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfCarDal : GenericRepository<Car>, ICarDal
{
    private readonly RentalyContext _context;
    public EfCarDal(RentalyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Car>> CarsWithCategoryAsync()
    {
        var values = await _context.Cars.Include(x => x.CarCategory)
            .Include(x=>x.Brand)
            .Include(x=>x.CarModel)
            .ToListAsync();
        return values;
    }

    public async Task<List<Car>> CarsWithStatusAsync()
    {
        var values = await _context.Cars.Where(x=>x.IsActive == true && x.IsAvailable == true).Include(x => x.CarCategory)
            .Include(x=>x.Brand)
            .Include(x=>x.CarModel)
            .ToListAsync();
        return values;
    }

    public async Task<List<Car>> GetFilteredCars(int? brandId, int? carCategoryId, string? fuel, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Cars
            .Include(x => x.Brand)
            .Include(x => x.CarCategory)
            .Include(x => x.CarModel)
            .AsQueryable();

        if (brandId.HasValue && brandId.Value > 0)
        {
            query = query.Where(x => x.BrandId == brandId.Value);
        }

        if (carCategoryId.HasValue && carCategoryId.Value > 0)
        {
            query = query.Where(x => x.CarCategoryId == carCategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(fuel))
        {
            query = query.Where(x => x.FuelType == fuel);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(x => x.DailyPrice >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(x => x.DailyPrice <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }
}