using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfBranchDal : GenericRepository<Branch>, IBranchDal
{
    public EfBranchDal(RentalyContext context) : base(context)
    {
    }
}