using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class FeedBackController : Controller
{
   private readonly AppDbContext _context;
   public IFeedbackRepository _feedbackRepository;

   public FeedBackController(AppDbContext context, IFeedbackRepository feedbackRepository)
   {
       _context = context;
       _feedbackRepository = feedbackRepository;
   }

   public async Task<IActionResult> Addfeedback(AddFeedBack feedBack)
   {
       var list = new Feedback();
       var first = await _context.User.FirstOrDefaultAsync(i => i.Id == feedBack.User_id);
       if (first != null)
       {
           list.User = first;
       }
       list.Name = feedBack.Name;
       list.Date = feedBack.Date;
       list.Phone_Number = feedBack.Phone_Number;
       var firstCourse = await _context.Course.FirstOrDefaultAsync(i => i.Id == feedBack.User_id);
       if (first != null)
       {
           list.Course = firstCourse;
       }

       await _feedbackRepository.CreateFeedback(list);
       var all = await _feedbackRepository.GetAllFeetback();
       return View("FeedbackTable", all);
   }


   public async Task<IActionResult> GetFeedback()
   {
       var all = await _feedbackRepository.GetAllFeetback();
       return View("FeedbackTable", all);
   }

   public async Task<IActionResult> Deletefeedback(int id)
   {
       if(!ModelState.IsValid) return View("FeedbackTable");

       await _feedbackRepository.DeleteFeedback(id);
       var all = await _feedbackRepository.GetAllFeetback();
       return View("FeedbackTable", all);
    }

   public async Task<IActionResult> GetById(int id)
   {
       if(!ModelState.IsValid) return View("FeedbackTable");
        
       var list = await _feedbackRepository.GetFeedbackById(id);
       return View("FeedBackUpdate", list);
    }
}
