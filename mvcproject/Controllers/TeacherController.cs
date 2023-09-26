using Microsoft.AspNetCore.Mvc;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class TeacherController : Controller
{
    private readonly ITeacherRepository _teacherRepository;
    public TeacherController (ITeacherRepository teacherRepository) => _teacherRepository = teacherRepository;

    [HttpGet]
    public async Task<IActionResult> GetReacher()
    {
        var list = await _teacherRepository.GetAllTeachers();
        return View("TeacherCard", list);
    }

    public async Task<IActionResult> AddTeacher(TeacherDto teacherDto)
    {
        var list = new Teacher();
        list.Id = teacherDto.Id;
        list.Url = teacherDto.Url;
        list.Name = teacherDto.Name;
        list.Description = teacherDto.Description;

        await _teacherRepository.CreateTecher(list);
        var listAll = await _teacherRepository.GetAllTeachers();
        return View("TeacherCard", listAll);
    }

    public async Task<IActionResult> GetById(int id)
    {
        var listAll = await _teacherRepository.GetTeacherById(id);
        return View("UpdateTeacher", listAll);
    }

    public async Task<IActionResult> Deleteteacher(int id)
    {
        if(!ModelState.IsValid) return View("TeacherCard");
        await _teacherRepository.DeleteTeacherById(id);
        var listAll = await _teacherRepository.GetAllTeachers();
        return View("TeacherCard", listAll);
    }

    public async Task<IActionResult> UpdateTeacher(int id, TeacherDto teacherDto)
    {
        var list = new TeacherDto();
        list.Url = teacherDto.Url;
        list.Name = teacherDto.Name;
        list.Description = teacherDto.Description;

        await _teacherRepository.UpdateTeacher(id , list);
        var listAll = await _teacherRepository.GetAllTeachers();
        return View("TeacherCard", listAll);
    }
}
