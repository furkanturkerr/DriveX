using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;

namespace RentalyNew.Areas.Admin.Controllers;
[Area("Admin")]
public class BranchController : Controller
{
    private readonly IBranchService _BranchBranch;

    public BranchController(IBranchService BranchBranch)
    {
        _BranchBranch = BranchBranch;
    }


    // GET
    public async Task<IActionResult> BranchList()
    {
        var values = await _BranchBranch.TGetListAsync();
        return View(values);
    }

    [HttpGet]
    public IActionResult CreateBranch()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateBranch(CreateBranchDto dto)
    {
        await _BranchBranch.TInsertAsync(dto);
        return RedirectToAction("BranchList");
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateBranch(int id)
    {
        var values = await _BranchBranch.TGetByIdAsync(id);
        return View(values);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateBranch(UpdateBranchDto dto)
    {
        await _BranchBranch.TUpdateAsync(dto);
        return RedirectToAction("BranchList");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBranch(int id)
    {
        await _BranchBranch.TDeleteAsync(id);
        return RedirectToAction("BranchList");
    }
}