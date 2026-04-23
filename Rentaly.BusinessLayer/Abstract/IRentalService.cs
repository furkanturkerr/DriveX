using Rentaly.DtoLayer.RentalDtos;

namespace Rentaly.BusinessLayer.Abstract;

public interface IRentalService : IGenericService<ResultRentalDto, CreateRentalDto, UpdateRentalDto>
{
}