namespace Rentaly.DtoLayer.CarModelDtos;

public class ResultCarModelWithBrand
{
    public int CarModelId { get; set; }
    public string ModelName { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; }
}