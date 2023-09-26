using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class TaskController : Controller
{
    private readonly AppDbContext _context;
    private readonly ITaskRepository _taskRepository;

    public TaskController(AppDbContext context, ITaskRepository taskRepository)
    {
        _context = context;
        _taskRepository = taskRepository;
    }
    public async Task<IActionResult> GetTask()
    {
        var all = await _taskRepository.GetAllTasks();
        return View("TaskTable", all);
    }

    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) return View("TaskTable");
        var all = await _taskRepository.GetTasksById(id);
        return View("Taskupdate", all);
    }

    public async Task<IActionResult> DeleteTask(int id)
    {
        if (!ModelState.IsValid) return View("TaskTable");

        await _taskRepository.DeleteTask(id);
        var all = await _taskRepository.GetAllTasks();
        return View("TaskTable", all);
    }

    public async Task<IActionResult> AddTask(AddTask addTask)
    {
        var list = new Enitiy.Task();
        list.Data = addTask.Data;
        list.Description = addTask.Description;
        list.Title = addTask.Title;
        list.complete = addTask.complete;
        var first = await _context.Lessons.FirstOrDefaultAsync(i => i.Id == addTask.LessonsId);
        if (first != null)
        {
            list.Lessons = first;
        }
        else throw new BadHttpRequestException("Not Found");


        await _taskRepository.CreateTask(list);
        var all = await _taskRepository.GetAllTasks();
        return View("TaskTable", all);
    }

    public async Task<IActionResult> UpdateTask(int id, AddTask addTask)
    {
        var list = new TaskDto();
        list.Data = addTask.Data;
        list.Description = addTask.Description;
        list.Title = addTask.Title;
        list.complete = addTask.complete;
        var first = await _context.Lessons.FirstOrDefaultAsync(i => i.Id == addTask.LessonsId);
        if (first != null)
        {
            list.Lessons = first;
        }
        else throw new BadHttpRequestException("Not Found");



        await _taskRepository.UpdateTask(id ,list); 
        var all = await _taskRepository.GetAllTasks();
        return View("TaskTable", all);
    }
}
