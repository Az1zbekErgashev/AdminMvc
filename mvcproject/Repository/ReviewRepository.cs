using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;


namespace mvcproject.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;
    public ReviewRepository(AppDbContext context) { _context = context; }
    public async Task CreateReview(Review review)
    {
        _context.Review.Add(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReview(int id)
    {
        var Cur = await _context.Review.FindAsync(id);
        if (Cur != null)
        {
            _context.Review.Remove(Cur);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ReviewDto>> GetAllReviews()
    {
        var Review = await _context.Review
            .Include(i => i.Task)
            .Select(i => new ReviewDto()
            {
                Course_info = i.Course_info,
                Course_name = i.Course_name,
                Id = i.Id,
                comment = i.comment,
                Task = i.Task,
            })

            .ToListAsync();
        return Review;
    }

    public async Task<Review> GetReviewById(int id)
    {
        var rev = await _context.Review
            .Include(i => i.Task)
            .FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var revi = new ReviewDto();
        revi.Id = id;
        revi.Course_info = rev.Course_info;
        revi.Course_name = rev.Course_name;
        revi.comment = rev.comment;
        revi.Task = rev.Task;

        return rev;
    }

    public async Task UpdateReview(int id, ReviewDto reviewDto)
    {
        var Cur = await _context.Review.FirstOrDefaultAsync(i => i.Id == id);
        if (Cur != null)
        {
            Cur.Course_name = reviewDto.Course_name;
            Cur.Course_info = reviewDto.Course_info;
            Cur.comment = reviewDto.comment;
            Cur.Task = reviewDto.Task;

            _context.Entry(Cur).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
