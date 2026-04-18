using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfBannerDal : GenericRepository<Banner>, IBannerDal
{
    public EfBannerDal(RentalyContext context) : base(context)
    {
    }
}