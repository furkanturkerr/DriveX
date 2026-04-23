using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.Repository;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.EntityFramework;

public class EfRentalDal : GenericRepository<Rental>, IRentalDal
{
    private readonly RentalyContext _context;

    public EfRentalDal(RentalyContext context) : base(context)
    {
        _context = context;
    }

    public async Task UpdateRentalStatusAndCarAsync(int rentalId, int status, string? adminNote)
    {
        var entity = await _context.Rentals.FindAsync(rentalId);

        if (entity == null)
            throw new Exception("Rezervasyon bulunamadı.");

        var car = await _context.Cars.FindAsync(entity.CarId);

        if (car == null)
            throw new Exception("Araç bulunamadı.");

        var newStatus = (RentalStatus)status;

        entity.Status = newStatus;
        entity.AdminNote = adminNote;

        if (newStatus == RentalStatus.Onaylandi)
        {
            entity.ApprovedAt = DateTime.Now;
            car.IsAvailable = false;
        }
        else if (newStatus == RentalStatus.Reddedildi
                 || newStatus == RentalStatus.Iptal
                 || newStatus == RentalStatus.Tamamlandi)
        {
            car.IsAvailable = true;
        }

        _context.Rentals.Update(entity);
        _context.Cars.Update(car);

        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> IsCarAvailableForDatesAsync(int carId, DateTime pickupDate, DateTime dropoffDate)
    {
        return !await _context.Rentals
            .Where(x => x.CarId == carId
                        && x.Status != RentalStatus.Reddedildi
                        && x.Status != RentalStatus.Iptal
                        && x.Status != RentalStatus.Tamamlandi
                        && x.PickupDate < dropoffDate
                        && x.DropoffDate > pickupDate)
            .AnyAsync();
    }
    
    public async Task<List<Rental>> GetRentalsByCarIdAsync(int carId)
    {
        return await _context.Rentals
            .Where(r => r.CarId == carId)
            .ToListAsync();
    }
    
    public async Task<List<int>> GetUnavailableCarIdsAsync(DateTime pickupDate, DateTime dropoffDate)
    {
        return await _context.Rentals
            .Where(x =>
                x.Status != RentalStatus.Reddedildi &&
                x.Status != RentalStatus.Iptal &&
                x.Status != RentalStatus.Tamamlandi &&
                x.PickupDate  < dropoffDate &&
                x.DropoffDate > pickupDate)
            .Select(x => x.CarId)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<Rental>> GetRentalWithDetailsAsync()
    {
        return await _context.Rentals
            .Include(x => x.Car)
            .ThenInclude(x => x.CarModel)
            .ThenInclude(x => x.Brand)
            .Include(x => x.PickupBranch)
            .Include(x => x.DropoffBranch)
            .ToListAsync();
    }
}