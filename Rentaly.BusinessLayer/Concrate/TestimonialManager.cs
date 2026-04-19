using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.TestimonialDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class TestimonialManager : ITestimonialService
{
    private readonly ITestimonialDal _testimonialDal;
    private readonly IMapper _mapper;

    public TestimonialManager(ITestimonialDal testimonialDal, IMapper mapper)
    {
        _testimonialDal = testimonialDal;
        _mapper = mapper;
    }

    public async Task<List<ResultTestimonialDto>> TGetListAsync()
    {
        var values  = await _testimonialDal.GetListAsync();
        return _mapper.Map<List<ResultTestimonialDto>>(values);
    }

    public async Task<UpdateTestimonialDto> TGetByIdAsync(int id)
    {
       var values = await _testimonialDal.GetByIdAsync(id);
       return _mapper.Map<UpdateTestimonialDto>(values);
    }

    public async Task TInsertAsync(CreateTestimonialDto dto)
    {
        var values = _mapper.Map<Testimonial>(dto);
        await _testimonialDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateTestimonialDto dto)
    {
        var values = _mapper.Map<Testimonial>(dto);
        await _testimonialDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _testimonialDal.DeleteAsync(id);
    }
}