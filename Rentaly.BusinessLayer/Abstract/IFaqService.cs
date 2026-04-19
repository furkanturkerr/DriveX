using Rentaly.DtoLayer.FaqDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Abstract;

public interface IFaqService : IGenericService<ResultFaqDto, CreateFaqDto, UpdateFaqDto>
{
    
}