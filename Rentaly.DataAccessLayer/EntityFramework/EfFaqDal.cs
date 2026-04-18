using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfFaqDal : GenericRepository<Faq>, IFaqDal
{
    public EfFaqDal(RentalyContext context) : base(context)
    {
    }
}