using AutoMapper;
using FluentValidation;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules.AboutValidations;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.AboutDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class AboutManager : IAboutService
{
    private readonly IAboutDal _aboutDal;
    private readonly IMapper _mapper;

    public AboutManager(IAboutDal aboutDal, IMapper mapper)
    {
        _aboutDal = aboutDal;
        _mapper = mapper;
    }

    public async Task<List<ResultAboutDto>> TGetListAsync()
    {
        var values = await _aboutDal.GetListAsync();
        return _mapper.Map<List<ResultAboutDto>>(values);
    }

    public async Task<UpdateAboutDto> TGetByIdAsync(int id)
    {
        var values = await _aboutDal.GetByIdAsync(id);
        return _mapper.Map<UpdateAboutDto>(values);
    }

    public async Task TInsertAsync(CreateAboutDto dto)
    {
        var validator = new CreateAboutValidator();
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = string.Join(" | ", result.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }

        var entity = _mapper.Map<About>(dto);
        await _aboutDal.InsertAsync(entity);
    }

    public async Task TUpdateAsync(UpdateAboutDto dto)
    {
        var validator = new UpdateAboutValidator();
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = string.Join(" | ", result.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }

        var entity = _mapper.Map<About>(dto);
        await _aboutDal.UpdateAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _aboutDal.DeleteAsync(id);
    }
}