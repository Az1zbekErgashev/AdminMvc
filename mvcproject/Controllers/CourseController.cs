using Microsoft.AspNetCore.Mvc;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class CourseController : Controller
{
    private readonly ICourseRepository _courseRepository;
    public CourseController(ICourseRepository courseRepository) => _courseRepository = courseRepository;
    [HttpGet]
    public async Task<IActionResult> GetCourse()
    {
        var all = await _courseRepository.GetCourseAllAsync();
        return View("CourseCard", all);
    }

    public async Task<IActionResult> AddCourse(CourseDto courseDto)
    {
        var course = new Course();
        course.Description = courseDto.Description;
        course.Name = courseDto.Name;
        course.Price = courseDto.Price;
        course.Url = courseDto.Url;
        await  _courseRepository.CreateCourseAsync(course);
        var all = await _courseRepository.GetCourseAllAsync();
        return View("CourseCard", all);
    }

    public async Task<IActionResult> UpdateCourse(int id, CourseDto courseDto)
    {
        if (!ModelState.IsValid) return View("CourseCard");

        var course = new CourseDto();

        course.Description = courseDto.Description;
        course.Name = courseDto.Name;
        course.Price = courseDto.Price;
        course.Url = courseDto.Url;

        await _courseRepository.UpdateCourseAsync(id,course);
        var all = await _courseRepository.GetCourseAllAsync();
        return View("CourseCard", all);

    }

    public async Task<IActionResult> DeleteCourse(int id)
    {
        if (!ModelState.IsValid) return View("CourseCard");

       await _courseRepository.DeleteCourseAsync(id);
        var all = await _courseRepository.GetCourseAllAsync();
        return View("CourseCard", all);
    }

    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) return View("CourseCard");

        var alsdsl = await _courseRepository.GetCourseByIdAsync(id);
        return View("UpdateCourse", alsdsl);

    }
}
