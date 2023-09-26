using Microsoft.AspNetCore.Mvc;

namespace mvcproject.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
