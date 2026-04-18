using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class ServiceManager : IServiceService
{
    private readonly IServiceDal _serviceDal;

    public ServiceManager(IServiceDal serviceDal)
    {
        _serviceDal = serviceDal;
    }

    public async Task TInsertAsync(Service entity)
    {
        await _serviceDal.InsertAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _serviceDal.DeleteAsync(id);
    }

    public async Task TUpdateAsync(Service entity)
    {
        await _serviceDal.UpdateAsync(entity);
    }

    public async Task<List<Service>> TGetListAsync()
    {
        return await _serviceDal.GetListAsync();
    }

    public async Task<Service> TGetByIdAsync(int id)
    {
        return await _serviceDal.GetByIdAsync(id);
    }
}