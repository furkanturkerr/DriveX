using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.ServiceDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class ServiceManager : IServiceService
{
    private readonly IServiceDal _serviceDal;
    private readonly IMapper _mapper;

    public ServiceManager(IServiceDal serviceDal, IMapper mapper)
    {
        _serviceDal = serviceDal;
        _mapper = mapper;
    }


    public async Task<List<ResultServiceDto>> TGetListAsync()
    {
        var values  = await _serviceDal.GetListAsync();
        return _mapper.Map<List<ResultServiceDto>>(values);
    }

    public async Task<UpdateServiceDto> TGetByIdAsync(int id)
    {
        var values = await _serviceDal.GetByIdAsync(id);
        return _mapper.Map<UpdateServiceDto>(values);
    }

    public async Task TInsertAsync(CreateServiceDto dto)
    {
        var values = _mapper.Map<Service>(dto);
        await _serviceDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateServiceDto dto)
    {
        var values = _mapper.Map<Service>(dto);
        await _serviceDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _serviceDal.DeleteAsync(id);
    }
}