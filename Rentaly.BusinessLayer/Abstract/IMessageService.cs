using Rentaly.DtoLayer.MessageDtos;

namespace Rentaly.BusinessLayer.Abstract;

public interface IMessageService : IGenericService<ResultMessageDto, CreateMessageDto, UpdateMessageDto>
{
    
}