using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfMessageDal : GenericRepository<Message>, IMessageDal
{
    public EfMessageDal(RentalyContext context) : base(context)
    {
    }
}