using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.FooterDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class FooterManager : IFooterService
{
    private readonly IFooterDal _footerDal;
    private readonly IMapper _mapper;
    
    public FooterManager(IFooterDal footerDal, IMapper mapper)
    {
        _footerDal = footerDal;
        _mapper = mapper;
    }

    public async Task<List<ResultFooterDto>> TGetListAsync()
    {
        var result = await _footerDal.GetListAsync();
        return _mapper.Map<List<ResultFooterDto>>(result);
    }

    public async Task<UpdateFooterDto> TGetByIdAsync(int id)
    {
        var result = await _footerDal.GetByIdAsync(id);
        return _mapper.Map<UpdateFooterDto>(result);
    }

    public async Task TInsertAsync(CreateFooterDto dto)
    {
        var entity = _mapper.Map<Footer>(dto);
        await _footerDal.InsertAsync(entity);
    }

    public async Task TUpdateAsync(UpdateFooterDto dto)
    {
        var entity = _mapper.Map<Footer>(dto);
        await _footerDal.UpdateAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _footerDal.DeleteAsync(id);
    }
}