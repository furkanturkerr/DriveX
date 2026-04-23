using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class CarManager : ICarService
{
    private readonly ICarDal _carDal;
    private readonly IMapper _mapper;

    public CarManager(ICarDal carDal, IMapper mapper)
    {
        _carDal = carDal;
        _mapper = mapper;
    }

    public async Task<List<ResultCarDto>> TGetListAsync()
    {
        var values = await _carDal.GetListAsync();
        return _mapper.Map<List<ResultCarDto>>(values);
    }

    public async Task<UpdateCarDto> TGetByIdAsync(int id)
    {
        var values = await _carDal.GetByIdAsync(id);
        return _mapper.Map<UpdateCarDto>(values);
    }

    public async Task TInsertAsync(CreateCarDto dto)
    {
        var entity = _mapper.Map<Car>(dto);
        await _carDal.InsertAsync(entity);
    }
    
    public async Task<ResultCarDto> TGetCarWithDetailsAsync(int id)
    {
        var values = await _carDal.GetCarWithDetailsAsync(id);
        return _mapper.Map<ResultCarDto>(values);
    }

    public async Task TUpdateAsync(UpdateCarDto dto)
    {
        var entity = _mapper.Map<Car>(dto);
        await _carDal.UpdateAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _carDal.DeleteAsync(id);
    }

    public async Task<List<ResultCarWithCategory>> TCarsWithCategoryAsync()
    {
        var values = await _carDal.CarsWithCategoryAsync();
        return _mapper.Map<List<ResultCarWithCategory>>(values);
    }

    public async Task<List<ResultCarWithCategory>> TCarsFilterAsync(CarFilterDto dto)
    {
        var values = await _carDal.GetFilteredCars(
            dto.BrandId,
            dto.CarCategoryId,
            dto.FuelType,
            dto.MinPrice,
            dto.MaxPrice
        );
        return _mapper.Map<List<ResultCarWithCategory>>(values);
    }
}