using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
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
}