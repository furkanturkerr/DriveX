using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class RentalManager : IRentalService
{
    private readonly IRentalDal _rentalDal;
    private readonly ICarDal    _carDal;
    private readonly IMapper    _mapper;

    public RentalManager(IRentalDal rentalDal, ICarDal carDal, IMapper mapper)
    {
        _rentalDal = rentalDal;
        _carDal    = carDal;
        _mapper    = mapper;
    }

    public async Task<List<ResultRentalDto>> TGetListAsync()
    {
        var result = await _rentalDal.GetListAsync();
        return _mapper.Map<List<ResultRentalDto>>(result);
    }

    public async Task<UpdateRentalDto> TGetByIdAsync(int id)
    {
        var result = await _rentalDal.GetByIdAsync(id);
        return _mapper.Map<UpdateRentalDto>(result);
    }

    public async Task TInsertAsync(CreateRentalDto dto)
    {
        var car    = await _carDal.GetByIdAsync(dto.CarId);
        var entity = _mapper.Map<Rental>(dto);

        entity.DailyPrice    = car.DailyPrice;
        entity.DepositAmount = car.DepositAmount;
        entity.TotalDays     = (dto.DropoffDate - dto.PickupDate).Days;
        entity.TotalPrice    = entity.DailyPrice * entity.TotalDays;
        entity.Status        = RentalStatus.Beklemede;
        entity.CreatedAt     = DateTime.Now;

        await _rentalDal.InsertAsync(entity);
    }

    public async Task TUpdateAsync(UpdateRentalDto dto)
    {
        var entity = await _rentalDal.GetByIdAsync(dto.RentalId);

        entity.Status    = (RentalStatus)dto.Status;
        entity.AdminNote = dto.AdminNote;

        if ((RentalStatus)dto.Status == RentalStatus.Onaylandi)
            entity.ApprovedAt = DateTime.Now;

        await _rentalDal.UpdateAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _rentalDal.DeleteAsync(id);
    }
}