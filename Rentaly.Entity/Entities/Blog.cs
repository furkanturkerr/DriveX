namespace Rentaly.Entity;

public class Blog
{
    public int BlogId { get; set; }
    public string Title { get; set; }        
    public string Description { get; set; } 
    public string ImageUrl { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsStatus { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}