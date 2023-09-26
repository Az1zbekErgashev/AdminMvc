using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using mvcproject.Model;
using mvcproject.Repository;

namespace mvcproject.Controllers;

public class ReviewController : Controller
{
    private readonly AppDbContext _context;
    private readonly IReviewRepository _reviewRepository;

    public ReviewController(AppDbContext context, IReviewRepository reviewRepository)
    {
        _context = context;
        _reviewRepository = reviewRepository;
    }

    public async Task<IActionResult> GetReview()
    {
        var all = await _reviewRepository.GetAllReviews();
        return View("Reviewtable", all);   
    }

    public async Task<IActionResult> GetById(int id)
    {
        var all = await _reviewRepository.GetReviewById(id);
        return View("ReviewUpdate", all);
    }

    public async Task<IActionResult> DeleteReview(int id)
    {
        if(!ModelState.IsValid) return View("Reviewtable");

        await _reviewRepository.DeleteReview(id);
        var all = await _reviewRepository.GetAllReviews();
        return View("Reviewtable", all);
    }

    public async Task<IActionResult> AddReview(AddReview addReview)
    {
        var list = new Review();
        list.Course_info = addReview.Course_info;
        list.Course_name = addReview.Course_name;
        list.comment = addReview.comment;

        var first = await _context.Task.FirstOrDefaultAsync(i => i.Id == addReview.TaskId);
        if (first != null)
        {
            list.Task = first;
        }

        await _reviewRepository.CreateReview(list);
        var all = await _reviewRepository.GetAllReviews();
        return View("Reviewtable", all);
    }


    public async Task<IActionResult> UpdateReview(int id, AddReview addReview)
    {
        var list = new ReviewDto();
        list.Course_info = addReview.Course_info;
        list.Course_name = addReview.Course_name;
        list.comment = addReview.comment;

        var first = await _context.Task.FirstOrDefaultAsync(i => i.Id == addReview.TaskId);
        if (first != null)
        {
            list.Task = first;
        }

        await _reviewRepository.UpdateReview(id ,list);
        var all = await _reviewRepository.GetAllReviews();
        return View("Reviewtable", all);
    }
}
