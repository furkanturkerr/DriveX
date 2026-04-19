using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.FaqDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class FaqManager : IFaqService
{
    private readonly IFaqDal _faqDal;
    private readonly IMapper _mapper;

    public FaqManager(IFaqDal faqDal, IMapper mapper)
    {
        _faqDal = faqDal;
        _mapper = mapper;
    }

    public async Task<List<ResultFaqDto>> TGetListAsync()
    {
        var values  = await _faqDal.GetListAsync();
        return _mapper.Map<List<ResultFaqDto>>(values);
    }

    public async Task<UpdateFaqDto> TGetByIdAsync(int id)
    {
        var values =  await _faqDal.GetByIdAsync(id);
        return _mapper.Map<UpdateFaqDto>(values);
    }

    public async Task TInsertAsync(CreateFaqDto dto)
    {
        var faq = _mapper.Map<Faq>(dto);
        await _faqDal.InsertAsync(faq);
    }

    public async Task TUpdateAsync(UpdateFaqDto dto)
    {
        var faq = _mapper.Map<Faq>(dto);
        await _faqDal.UpdateAsync(faq);
    }

    public async Task TDeleteAsync(int id)
    {
       await _faqDal.DeleteAsync(id);
    }
}