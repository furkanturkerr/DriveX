namespace Rentaly.Entity;

public class CarCategory
{
    public int CarCategoryId { get; set; }
    public string CarCategoryName { get; set; }
    public List<Car> Cars { get; set; }
}