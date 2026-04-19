using Rentaly.DtoLayer.BannerDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Abstract;

public interface IBannerService : IGenericService<ResultBannerDto, CreateBannerDto, UpdateBannerDto>
{
    
}