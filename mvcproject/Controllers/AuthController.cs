using Microsoft.AspNetCore.Mvc;
using mvcproject.Authservis;
using mvcproject.Dto;

namespace mvcproject.Controllers;

public class AuthController : Controller
{
    private readonly JwtServis _userRepository;
    public AuthController(JwtServis userRepository)
    {
        _userRepository = userRepository;
    }



    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return View(loginDto);

        bool login =_userRepository.Login(loginDto);

        if (login)
        {
            if (loginDto.Email == "aziz@gmail.com" && loginDto.Password == "AdminRoot" && loginDto.Name == "Root")
            {
                return RedirectToAction("GetUsers", "User");
            }


        }

        return RedirectToAction("Login", "Auth");
    }

}
