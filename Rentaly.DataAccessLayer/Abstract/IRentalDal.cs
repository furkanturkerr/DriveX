using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Abstract;

public interface IRentalDal : IGenericDal<Rental>
{
    Task UpdateRentalStatusAndCarAsync(int rentalId, int status, string? adminNote);
    Task<Rental> GetRentalWithDetailsAsync(int id);
}