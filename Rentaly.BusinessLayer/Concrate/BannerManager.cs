using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class BannerManager : IBannerService
{
    private readonly IBannerDal _bannerDal;

    public BannerManager(IBannerDal bannerDal)
    {
        _bannerDal = bannerDal;
    }

    public async Task TInsertAsync(Banner entity)
    {
        await _bannerDal.InsertAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _bannerDal.DeleteAsync(id);
    }

    public async Task TUpdateAsync(Banner entity)
    {
        await _bannerDal.UpdateAsync(entity);
    }

    public async Task<List<Banner>> TGetListAsync()
    {
        return await _bannerDal.GetListAsync();
    }

    public async Task<Banner> TGetByIdAsync(int id)
    {
        return await _bannerDal.GetByIdAsync(id);
    }
}