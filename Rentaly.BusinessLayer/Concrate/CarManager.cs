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

    public async Task<CarListPagedResultDto> TGetPagedCarListAsync(CarFilterRequestDto filter)
    {
        var values = await _carDal.CarsWithCategoryAsync();
        var all = _mapper.Map<List<ResultCarWithCategory>>(values);

        var filtered = all.AsEnumerable();

        if (!string.IsNullOrEmpty(filter.Brand))
            filtered = filtered.Where(x => x.BrandName == filter.Brand);

        if (!string.IsNullOrEmpty(filter.Category))
            filtered = filtered.Where(x => x.CategoryName == filter.Category);

        if (!string.IsNullOrEmpty(filter.Fuel))
            filtered = filtered.Where(x => x.FuelType == filter.Fuel);

        if (filter.MinPrice.HasValue)
            filtered = filtered.Where(x => x.DailyPrice >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            filtered = filtered.Where(x => x.DailyPrice <= filter.MaxPrice.Value);

        var filteredList = filtered.ToList();

        int totalItems = filteredList.Count;
        int totalPages = (int)Math.Ceiling(totalItems / (double)filter.PageSize);
        int page = Math.Max(1, Math.Min(filter.Page, Math.Max(1, totalPages)));

        var pagedCars = filteredList
            .Skip((page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new CarListPagedResultDto
        {
            Cars = pagedCars,
            AllBrands = all.Select(x => x.BrandName).Distinct().OrderBy(x => x).ToList(),
            AllCategories = all.Select(x => x.CategoryName).Distinct().OrderBy(x => x).ToList(),
            AllFuelTypes = all.Select(x => x.FuelType).Distinct().OrderBy(x => x).ToList(),

            BrandFilter = filter.Brand,
            CategoryFilter = filter.Category,
            FuelFilter = filter.Fuel,
            MinPrice = filter.MinPrice,
            MaxPrice = filter.MaxPrice,

            CurrentPage = page,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }
}