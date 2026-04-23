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
}