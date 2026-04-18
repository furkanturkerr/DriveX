using Microsoft.EntityFrameworkCore;
using Rentaly.Entity;

namespace Rentaly.DataAccessLayer.Concrete;

public class RentalyContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1995;Database=RentalyNew;User Id=sa;Password=Furkan12*;TrustServerCertificate=True;");
    }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<About> Abouts { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Faq> Faqs { get; set; }
    public DbSet<Footer> Footers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
}