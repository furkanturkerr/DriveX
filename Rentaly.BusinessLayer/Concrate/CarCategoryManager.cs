using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.CarCategoryDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class CarCategoryManager : ICarCategoryService
{
    private readonly ICarCategoryDal _carCategoryDal;
    private readonly IMapper _mapper;

    public CarCategoryManager(ICarCategoryDal carCategoryDal, IMapper mapper)
    {
        _carCategoryDal = carCategoryDal;
        _mapper = mapper;
    }

    public async Task<List<ResultCarCategoryDto>> TGetListAsync()
    {
        var values = await _carCategoryDal.GetListAsync();
        return _mapper.Map<List<ResultCarCategoryDto>>(values);
    }

    public async Task<UpdateCarCategoryDto> TGetByIdAsync(int id)
    {
        var values = await _carCategoryDal.GetByIdAsync(id);
        return _mapper.Map<UpdateCarCategoryDto>(values);
    }

    public async Task TInsertAsync(CreateCarCategoryDto dto)
    {
        var values = _mapper.Map<CarCategory>(dto);
        await _carCategoryDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateCarCategoryDto dto)
    {
        var values = _mapper.Map<CarCategory>(dto);
        await _carCategoryDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _carCategoryDal.DeleteAsync(id);
    }
}