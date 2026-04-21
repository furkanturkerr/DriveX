using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfCarModelDal : GenericRepository<CarModel>, ICarModelDal
{
    private readonly RentalyContext _context;
    public EfCarModelDal(RentalyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CarModel>> GetCarModelsWithBrandAsync()
    {
        var values = await _context.CarModels.Include(x=>x.Brand).ToListAsync();
        return values;
    }
}