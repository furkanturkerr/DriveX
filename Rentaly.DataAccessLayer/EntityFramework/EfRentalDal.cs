using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfRentalDal : GenericRepository<Rental>, IRentalDal
{
    public EfRentalDal(RentalyContext context) : base(context)
    {
    }
}