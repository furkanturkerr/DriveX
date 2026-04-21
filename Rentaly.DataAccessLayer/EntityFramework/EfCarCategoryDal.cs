using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfCarCategoryDal : GenericRepository<CarCategory>, ICarCategoryDal
{
    public EfCarCategoryDal(RentalyContext context) : base(context)
    {
    }
}