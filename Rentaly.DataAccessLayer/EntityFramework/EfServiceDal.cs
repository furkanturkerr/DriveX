using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfServiceDal : GenericRepository<Service>, IServiceDal
{
    public EfServiceDal(RentalyContext context) : base(context)
    {
    }
}