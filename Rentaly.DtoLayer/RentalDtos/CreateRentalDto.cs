namespace Rentaly.DtoLayer.RentalDtos;

public class CreateRentalDto
{
    public int CarId { get; set; }
 
    public string   FirstName      { get; set; }
    public string   LastName       { get; set; }
    public string   IdentityNumber { get; set; }
    public string   Phone          { get; set; }
    public string   Email          { get; set; }
    public string   LicenseNumber  { get; set; }
    public DateTime BirthDate      { get; set; }
 
    public int      PickupBranchId  { get; set; }
    public int      DropoffBranchId { get; set; }
    public DateTime PickupDate      { get; set; }
    public DateTime DropoffDate     { get; set; }
}