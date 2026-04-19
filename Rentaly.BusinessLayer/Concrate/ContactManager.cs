using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.ContactDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class ContactManager : IContactService
{
    private readonly IContactDal _contactDal;
    private readonly IMapper _mapper;

    public ContactManager(IContactDal contactDal, IMapper mapper)
    {
        _contactDal = contactDal;
        _mapper = mapper;
    }

    public async Task<List<ResultContactDto>> TGetListAsync()
    {
        var result = await _contactDal.GetListAsync();
        return _mapper.Map<List<ResultContactDto>>(result);
    }

    public async Task<UpdateContactDto> TGetByIdAsync(int id)
    {
        var result = await _contactDal.GetByIdAsync(id);
        return _mapper.Map<UpdateContactDto>(result);
    }

    public async Task TInsertAsync(CreateContactDto dto)
    {
        var entity = _mapper.Map<Contact>(dto);
        await _contactDal.InsertAsync(entity);
    }

    public async Task TUpdateAsync(UpdateContactDto dto)
    {
        var entity = _mapper.Map<Contact>(dto);
        await _contactDal.UpdateAsync(entity);
    }

    public async Task TDeleteAsync(int id)
    {
        await _contactDal.DeleteAsync(id);
    }
}