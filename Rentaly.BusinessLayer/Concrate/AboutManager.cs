using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class AboutManager : IAboutService
{
    private readonly IAboutDal _aboutDal;

    public AboutManager(IAboutDal aboutDal)
    {
        _aboutDal = aboutDal;
    }

    public async Task TInsertAsync(About entity)
    {
        await _aboutDal.InsertAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _aboutDal.DeleteAsync(id);
    }

    public async Task TUpdateAsync(About entity)
    {
        await _aboutDal.UpdateAsync(entity);
    }

    public async Task<List<About>> TGetListAsync()
    {
        return await _aboutDal.GetListAsync();
    }

    public async Task<About> TGetByIdAsync(int id)
    {
        return await _aboutDal.GetByIdAsync(id);
    }
}