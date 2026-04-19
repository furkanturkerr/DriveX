using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
{
    public EfCategoryDal(RentalyContext context) : base(context)
    {
    }
}