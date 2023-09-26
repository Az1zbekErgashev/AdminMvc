using Microsoft.AspNetCore.Mvc;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userrepository;
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IResultRepository _resultRepository;
    public UserController(IUserRepository userrepository, IFeedbackRepository feedbackRepository, IResultRepository resultRepository)
    {
        _userrepository = userrepository;
        _feedbackRepository = feedbackRepository;
        _resultRepository = resultRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    
    {
        var all = await _userrepository.GetAllUsers();
        return View("UserTable", all);
    }

    public async Task<IActionResult> AddUser(UserDto userDto)
    {
        if(!ModelState.IsValid) return View("UserTable");

        var user = new User();
        user.Email = userDto.Email;
        user.FullName = userDto.FullName;
        user.Password = userDto.Password;

        await _userrepository.CreateUsers(user);
        var all = await _userrepository.GetAllUsers();
        return View("UserTable", all);
    }

    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) return View("UserTable");

            var user =  await _userrepository.GetById(id);
            return View("UpdateUser", user);

    }

    public async Task<IActionResult> DeleteUser(int id)
    {
        if(!ModelState.IsValid) return View("UserTable");

        await _userrepository.DeleteUsers(id);
        var all = await _userrepository.GetAllUsers();
        return View("UserTable", all);
    }

    public async Task<IActionResult> UpdateUser()
    {
        return View("UpdateUser");
    }
    public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
    {
        if (!ModelState.IsValid) return View("UserTable");

        var user = new UserDto();
        user.Email = userDto.Email;
        user.FullName = userDto.FullName;
        user.Password = userDto.Password;

        await _userrepository.UpdateUsers(id, user);
        var all = await _userrepository.GetAllUsers();
        return View("UserTable", all);

    }


    public async Task<IActionResult> UserCourse(int id)
    {
        if (!ModelState.IsValid) return View("UserTable");
        var all = await _userrepository.GetUserCourse(id);
        return View("UserCourse", all);
    }

    public async Task<IActionResult> UserFeedback(int id)
    {
        if (!ModelState.IsValid) return View("UserTable");
        var all = await _feedbackRepository.GetUserFeedback(id);
        return View("UserFeedback", all);
    }  
    
    public async Task<IActionResult> UserResult(int id)
    {
        if (!ModelState.IsValid) return View("UserTable");
        var all = await _resultRepository.GetUserResult(id);
        return View("UserResult", all);
    }
}
