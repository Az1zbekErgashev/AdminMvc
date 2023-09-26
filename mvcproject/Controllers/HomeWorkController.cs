using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class HomeWorkController : Controller
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly AppDbContext _appDbContext;

    public HomeWorkController(IHomeWorkRepository homeWorkRepository, AppDbContext appDbContext)
    {
        _homeWorkRepository = homeWorkRepository;
        _appDbContext = appDbContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetHomeWork()
    {
        var all = await _homeWorkRepository.GetAllHomeWork();
        return View("HomeWorkTable", all);
    }

    public async Task<IActionResult> AddHomewWork(AddHomeWork homeWorkDto)
    {
        var course = new HomeWork();
        course.Img = homeWorkDto.Img;
        course.Name = homeWorkDto.Name;
        var list = new Education();
        var findCourse = await _appDbContext.Task.FirstOrDefaultAsync(c => c.Id == homeWorkDto.TaskId);
        if (findCourse != null)
        {
            course.Task = findCourse;

        }
        else throw new BadHttpRequestException("Not Found");

        course.Title = homeWorkDto.Title;

        await _homeWorkRepository.CreateHome(course);
        var all = await _homeWorkRepository.GetAllHomeWork();
        return View("HomeWorkTable", all);
    }

    public async Task<IActionResult> UpdateHomeWork(int id, AddHomeWork homeWorkDto)
    {
        if (!ModelState.IsValid) return View("HomeWorkTable");

        var course = new HomeWorkDto();
        var findCourse = await _appDbContext.Task.FirstOrDefaultAsync(c => c.Id == homeWorkDto.TaskId);
        if (findCourse != null)
        {
            course.Task = findCourse;

        }
        else throw new BadHttpRequestException("Not Found");

        course.Img = homeWorkDto.Img;
        course.Name = homeWorkDto.Name;
        course.Title = homeWorkDto.Title;

        await _homeWorkRepository.UpdateHomeWork(id, course);
        var all = await _homeWorkRepository.GetAllHomeWork();
        return View("HomeWorkTable", all);

    }

    public async Task<IActionResult> DeleteHomeWork(int id)
    {
        if (!ModelState.IsValid) return View("HomeWorkTable");

        await _homeWorkRepository.DeleteHomeWorkById(id);
        var all = await _homeWorkRepository.GetAllHomeWork();
        return View("HomeWorkTable", all);
    }

    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) return View("HomeWorkTable");

        var alsdsl = await _homeWorkRepository.GetHomeWorkById(id);
        return View("UpdateHome", alsdsl);

    }
}
