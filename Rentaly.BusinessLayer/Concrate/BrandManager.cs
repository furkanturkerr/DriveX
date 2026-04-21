using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.BrandDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class BrandManager : IBrandService
{
    private readonly IBrandDal _brandDal;
    private readonly IMapper _mapper;

    public BrandManager(IBrandDal brandDal, IMapper mapper)
    {
        _brandDal = brandDal;
        _mapper = mapper;
    }

    public async Task<List<ResultBrandDto>> TGetListAsync()
    {
        var values = await _brandDal.GetListAsync();
        return _mapper.Map<List<ResultBrandDto>>(values);
    }

    public async Task<UpdateBrandDto> TGetByIdAsync(int id)
    {
        var values = await _brandDal.GetByIdAsync(id);
        return _mapper.Map<UpdateBrandDto>(values);
    }

    public async Task TInsertAsync(CreateBrandDto dto)
    {
        var values = _mapper.Map<Brand>(dto);
        await _brandDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateBrandDto dto)
    {
        var values = _mapper.Map<Brand>(dto);
        await _brandDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _brandDal.DeleteAsync(id);
    }
}