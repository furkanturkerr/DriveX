using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.BannerDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class BannerManager : IBannerService
{
    private readonly IBannerDal _bannerDal;
    private readonly IMapper _mapper;

    public BannerManager(IBannerDal bannerDal, IMapper mapper)
    {
        _bannerDal = bannerDal;
        _mapper = mapper;
    }

    public async Task<List<ResultBannerDto>> TGetListAsync()
    {
        var values = await _bannerDal.GetListAsync();
        return _mapper.Map<List<ResultBannerDto>>(values);
    }

    public async Task<UpdateBannerDto> TGetByIdAsync(int id)
    {
        var values = await _bannerDal.GetByIdAsync(id);
        return _mapper.Map<UpdateBannerDto>(values);
    }

    public async Task TInsertAsync(CreateBannerDto dto)
    {
        var values = _mapper.Map<Banner>(dto);
        await _bannerDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateBannerDto dto)
    {
        var values = _mapper.Map<Banner>(dto);
        await _bannerDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _bannerDal.DeleteAsync(id);
    }
}