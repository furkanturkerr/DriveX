using Rentaly.DtoLayer.RentalDtos;

namespace Rentaly.BusinessLayer.Abstract;

public interface IRentalService : IGenericService<ResultRentalDto, CreateRentalDto, UpdateRentalDto>
{
    Task<List<int>> TGetUnavailableCarIdsAsync(DateTime pickupDate, DateTime dropoffDate);
    Task<List<ResultRentalDto>> GetRentalWithDetailsAsync();
    Task<List<object>> TGetBookedDatesAsync(int carId);
}