namespace Rentaly.DtoLayer.ServiceDtos;

public class CreateServiceDto
{
    public string Title { get; set; }        
    public string Description { get; set; } 
    public string IconUrl { get; set; }

    public string Position { get; set; }
}