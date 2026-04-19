using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfFooterDal : GenericRepository<Footer>, IFooterDal
{
    public EfFooterDal(RentalyContext context) : base(context)
    {
    }
}