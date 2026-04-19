namespace Rentaly.DtoLayer.BlogDtos;

public class CreateBlogDto
{
    public string Title { get; set; }        
    public string Description { get; set; } 
    public string ImageUrl { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsStatus { get; set; }
    public int CategoryId { get; set; }
}