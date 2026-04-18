namespace Rentaly.Entity;

public class Brand
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public string ImageUrl { get; set; }
    
    public List<Car> Cars { get; set; }
}