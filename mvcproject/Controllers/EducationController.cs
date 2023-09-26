using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;
using System.Collections.Generic;

namespace mvcproject.Controllers;

public class EducationController : Controller
{
    private readonly IEducationRepository _educationRepository;
    private readonly AppDbContext _appDbContext;

    public EducationController(IEducationRepository educationRepository, AppDbContext appDbContext)
    {
        _educationRepository = educationRepository;
        _appDbContext = appDbContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetEducation()
    {

        var list = await _educationRepository.GetAllEducation();
        return View("EducationTable", list);

    }

    public async Task<IActionResult> AddEducation(AddEducation educationDto)
    {
        var list = new Education();
        var findCourse = await _appDbContext.Course.FirstOrDefaultAsync(c => c.Id == educationDto.CourseID);
        if (findCourse != null)
        {
            list.course = findCourse;
          
        }



        list.Description = educationDto.Description;
        list.courseСompletion = new List<string>();

        if (educationDto.courseСompletion != null)
        {
            list.courseСompletion.AddRange(educationDto.courseСompletion);
        }
        list.Tranding = educationDto.Tranding;

        await _educationRepository.CreateEducation(list);
        var listAll = await _educationRepository.GetAllEducation();
        return View("EducationTable", listAll);
    }


    public async Task<IActionResult> GetByid(int id)
    {
        var list = await _educationRepository.GetEducationById(id);
        return View("EducationUpdate" , list);
    }

    public async Task<IActionResult> DeleteEducation(int id)
    {
        if(!ModelState.IsValid) return View("EducationTable");


         await _educationRepository.DeleteEducationById(id);
         var list = await _educationRepository.GetAllEducation();
        return View("EducationTable", list);

    }

    public async Task<IActionResult> UpdateEducation(int id , AddEducation educationDto)
    {
        if (!ModelState.IsValid) return View("EducationTable");

        var list = new EducationDto();
        var findCourse = await _appDbContext.Course.FirstOrDefaultAsync(c => c.Id == educationDto.CourseID);
        if (findCourse != null)
        {
            list.Course = findCourse;
        }
        list.Description = educationDto.Description;
        list.courseСompletion = new List<string>(educationDto.courseСompletion);
        list.Tranding = educationDto.Tranding;

        await _educationRepository.UpdateEducation(id, list);
        var listAll = await  _educationRepository.GetAllEducation();
        return View("EducationTable", listAll);
    }
}
