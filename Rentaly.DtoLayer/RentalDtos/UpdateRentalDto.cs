namespace Rentaly.DtoLayer.RentalDtos;

public class UpdateRentalDto
{
    public int    RentalId   { get; set; }
    public int    Status     { get; set; }
    public string? AdminNote { get; set; }
}