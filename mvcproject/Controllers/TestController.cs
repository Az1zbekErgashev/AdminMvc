using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class TestController : Controller
{
    private readonly AppDbContext _context;
    private readonly ITestRepository _testRepository;

    public TestController(AppDbContext context, ITestRepository testRepository)
    {
        _context = context;
        _testRepository = testRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetTest()
    {
        var all = await _testRepository.GetAllTest();
        return View("TestTable", all);
    }

    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) return View("TestTable");
        var all = await _testRepository.GetTestById(id);
        return View("TestTable");
    }

    public async Task<IActionResult> DeleteTets(int id)
    {
        if (!ModelState.IsValid) return View("TestTable");
        await _testRepository.DeleteTest(id);
        var all = await _testRepository.GetAllTest();
        return View("TestTable", all);
    }

    public async Task<IActionResult> AddTest(AddTests addTests)
    {
        var list = new Tests();
        list.Queshioquestion = addTests.Queshioquestion;
        list.correct = addTests.correct;
        list.incorrect = new List<string>();
        if (addTests.incorrect != null)
        {
                list.incorrect.AddRange(addTests.incorrect);
        }

        var first = await _context.Course.FirstOrDefaultAsync(i => i.Id == addTests.CourseId);
        if (first != null)
        {
            list.Course = first;
        }

        await _testRepository.CreateTest(list);
        var all = await _testRepository.GetAllTest();
        return View("TestTable", all);
    }

    public async Task<IActionResult> UpdateTest(int id, AddTests addTests)
    {
        var list = new TestsDto();
        list.Queshioquestion = addTests.Queshioquestion;
        list.correct = addTests.correct;
        list.incorrect = new List<string>();
        if (addTests.incorrect != null)
        {
            list.incorrect.AddRange(addTests.incorrect);
        }

        var first = await _context.Course.FirstOrDefaultAsync(i => i.Id == addTests.CourseId);
        if (first != null)
        {
            list.Course = first;
        }

        await _testRepository.UpdateTest(id , list);
        var all = await _testRepository.GetAllTest();
        return View("TestTable", all);
    }
}
