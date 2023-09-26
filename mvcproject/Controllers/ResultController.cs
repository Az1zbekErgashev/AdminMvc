using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class ResultController : Controller
{
    private readonly AppDbContext _context;
    private readonly IResultRepository _resultRepository;

    public ResultController(AppDbContext context, IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
        _context = context;
    }

    public async Task<IActionResult> GetResult()
    {
        var all = await _resultRepository.GetAllResults();
        return View("ResultTable", all);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var all = await _resultRepository.GetResultById(id);
        return View("ResultUpdate", all);
    }

    public async Task<IActionResult> AddResult(AddResult addResult)
    {
        var list = new Result();
        list.Url = addResult.Url;
        list.Name = addResult.Name;
        var first = await _context.Education.FirstOrDefaultAsync(i => i.Id == addResult.EducationId);
        if (first != null)
        {
            list.Education = first;
        }
        else throw new BadHttpRequestException("Not Found");


        var firstUser = await _context.User.FirstOrDefaultAsync(i => i.Id == addResult.UserId);
        if (firstUser != null)
        {
            list.User = firstUser;
        }
        else throw new BadHttpRequestException("Not Found");


        await _resultRepository.CreateResult(list);
        var all = await _resultRepository.GetAllResults();
        return View("ResultTable", all);
    }

    public async Task<IActionResult> UpdateResult(int id, AddResult addResult)
    {
        var list = new ResultDto();
        list.Url = addResult.Url;
        list.Name = addResult.Name;
        var first = await _context.Education.FirstOrDefaultAsync(i => i.Id == addResult.EducationId);
        if (first != null)
        {
            list.Education = first;
        }
        else throw new BadHttpRequestException("Not Found");
        var firstUser = await _context.User.FirstOrDefaultAsync(i => i.Id == addResult.UserId);
        if (firstUser != null)
        {
            list.User = firstUser;
        }
        else throw new BadHttpRequestException("Not Found");



        await _resultRepository.UpdateResult(id , list);
        var all = await _resultRepository.GetAllResults();
        return View("ResultTable", all);
    }

    public async Task<IActionResult> DeleteResult(int id)
    {
        if (!ModelState.IsValid) return View("ResultTable");

        await _resultRepository.DeleteResultById(id);
        var all = await _resultRepository.GetAllResults();
        return View("ResultTable", all);
    }
}
