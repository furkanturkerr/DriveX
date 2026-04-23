using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Abstract;

public interface IRentalDal : IGenericDal<Rental>
{
    Task UpdateRentalStatusAndCarAsync(int rentalId, int status, string? adminNote);
    Task<List<Rental>> GetRentalWithDetailsAsync();
    Task<bool> IsCarAvailableForDatesAsync(int carId, DateTime pickupDate, DateTime dropoffDate);
    Task<List<int>> GetUnavailableCarIdsAsync(DateTime pickupDate, DateTime dropoffDate);
    Task<List<Rental>> GetRentalsByCarIdAsync(int carId);
}