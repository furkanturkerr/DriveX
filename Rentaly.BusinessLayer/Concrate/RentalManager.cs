using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class RentalManager : IRentalService
{
    private readonly IRentalDal _rentalDal;
    private readonly ICarDal _carDal;
    private readonly IMapper _mapper;

    public RentalManager(IRentalDal rentalDal, ICarDal carDal, IMapper mapper)
    {
        _rentalDal = rentalDal;
        _carDal = carDal;
        _mapper = mapper;
    }

    public async Task<List<ResultRentalDto>> TGetListAsync()
    {
        var values = await _rentalDal.GetListAsync();
        return _mapper.Map<List<ResultRentalDto>>(values);
    }

    public async Task<UpdateRentalDto> TGetByIdAsync(int id)
    {
        var value = await _rentalDal.GetByIdAsync(id);
        return _mapper.Map<UpdateRentalDto>(value);
    }

    public async Task TInsertAsync(CreateRentalDto dto)
    {
        var car = await _carDal.GetByIdAsync(dto.CarId);

        if (car == null)
            throw new Exception("Araç bulunamadı.");

        if (dto.PickupDate == default || dto.DropoffDate == default)
            throw new Exception("Tarih bilgisi eksik.");

        var totalDays = (dto.DropoffDate - dto.PickupDate).Days;

        if (totalDays <= 0)
            throw new Exception("Teslim tarihi alış tarihinden sonra olmalıdır.");

        var entity = _mapper.Map<Rental>(dto);

        entity.DailyPrice = car.DailyPrice;
        entity.DepositAmount = car.DepositAmount;
        entity.TotalDays = totalDays;
        entity.TotalPrice = entity.DailyPrice * totalDays;
        entity.Status = RentalStatus.Beklemede;
        entity.CreatedAt = DateTime.Now;

        await _rentalDal.InsertAsync(entity);
    }

    public async Task TUpdateAsync(UpdateRentalDto dto)
    {
        await _rentalDal.UpdateRentalStatusAndCarAsync(dto.RentalId, dto.Status, dto.AdminNote);
    }

    public async Task TDeleteAsync(int id)
    {
        await _rentalDal.DeleteAsync(id);
    }
}