namespace Rentaly.Entity;

public class Branch
{
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    
    public List<Car> Cars { get; set; }
}