using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class LessonController : Controller
{
    private readonly AppDbContext _context;
    private ILessonsRepository _lessonsRepository;

    public LessonController(AppDbContext context, ILessonsRepository lessonsRepository)
    {
        _context = context;
        _lessonsRepository = lessonsRepository;
    }

    public async Task<IActionResult> GetLesson()
    {
        var all = await _lessonsRepository.GetAllLessons();
        return View("LessonTable", all);
    }

    public async Task<IActionResult> GetByid(int id)
    {
        var all = await _lessonsRepository.GetLessonsById(id);
        return View("UpdateLeeson", all);
    }

    public async Task<IActionResult> AddLesson(AddLesson addLesson)
    {
        var list = new Lessons();
        list.Description = addLesson.Description;
        list.Title = addLesson.Title;
        list.Vedeo_url2 = addLesson.Vedeo_url2;
        list.Video_url = addLesson.Video_url;
        var first = await _context.Course.FirstOrDefaultAsync(i => i.Id == addLesson.CourseId);
        if(first != null)
        {
            list.Course = first;
        }

        await _lessonsRepository.CreateLesson(list);
        var all = await _lessonsRepository.GetAllLessons();
        return View("LessonTable", all);
    }

    public async Task<IActionResult> DeleeteLesson(int id)
    {
        if (!ModelState.IsValid) return View("LessonTable");

        await _lessonsRepository.DeleteLessonsById(id);
        var all = await _lessonsRepository.GetAllLessons();
        return View("LessonTable", all);
    }

    public async Task<IActionResult> UpdateLesson(int id, AddLesson addLesson)
    {
        var list = new LessonsDto();
        list.Description = addLesson.Description;
        list.Title = addLesson.Title;
        list.Vedeo_url2 = addLesson.Vedeo_url2;
        list.Video_url = addLesson.Video_url;
        var first = await _context.Course.FirstOrDefaultAsync(i => i.Id == addLesson.CourseId);
        if (first != null)
        {
            list.Course = first;
        }

        await _lessonsRepository.UpdateLessons(id , list);
        var all = await _lessonsRepository.GetAllLessons();
        return View("LessonTable", all);
    }
}
