using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.CarModelDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class CarModelManager : ICarModelService
{
    private readonly ICarModelDal _carModelDal;
    private readonly IMapper _mapper;

    public CarModelManager(ICarModelDal carModelDal, IMapper mapper)
    {
        _carModelDal = carModelDal;
        _mapper = mapper;
    }


    public async Task<List<ResultCarModelDto>> TGetListAsync()
    {
        var values = await _carModelDal.GetListAsync();
        return _mapper.Map<List<ResultCarModelDto>>(values);
    }

    public async Task<UpdateCarModelDto> TGetByIdAsync(int id)
    {
        var values = await _carModelDal.GetByIdAsync(id);
        return _mapper.Map<UpdateCarModelDto>(values);
    }

    public async Task TInsertAsync(CreateCarModelDto dto)
    {
        var values = _mapper.Map<CarModel>(dto);
        await _carModelDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateCarModelDto dto)
    {
        var values = _mapper.Map<CarModel>(dto);
        await _carModelDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _carModelDal.DeleteAsync(id);
    }

    public async Task<List<ResultCarModelWithBrand>> GetCarModelsWithBrandAsync()
    {
        var values = await _carModelDal.GetCarModelsWithBrandAsync();
        return _mapper.Map<List<ResultCarModelWithBrand>>(values);
    }
}