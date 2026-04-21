using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.Entity;

namespace Rentaly.BusinessLayer.Concrate;

public class BranchManager : IBranchService
{
    private readonly IBranchDal _branchDal;
    private readonly IMapper _mapper;

    public BranchManager(IBranchDal branchDal, IMapper mapper)
    {
        _branchDal = branchDal;
        _mapper = mapper;
    }

    public async Task<List<ResultBranchDto>> TGetListAsync()
    {
        var values = await _branchDal.GetListAsync();
        return _mapper.Map<List<ResultBranchDto>>(values);
    }

    public async Task<UpdateBranchDto> TGetByIdAsync(int id)
    {
        var values = await _branchDal.GetByIdAsync(id);
        return _mapper.Map<UpdateBranchDto>(values);
    }

    public async Task TInsertAsync(CreateBranchDto dto)
    {
        var values = _mapper.Map<Branch>(dto);
        await _branchDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateBranchDto dto)
    {
        var values = _mapper.Map<Branch>(dto);
        await _branchDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _branchDal.DeleteAsync(id);
    }
}