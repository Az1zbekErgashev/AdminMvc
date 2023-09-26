using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public class FeedBackRepository : IFeedbackRepository
    {
        private AppDbContext _context;
        public FeedBackRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateFeedback(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedback(int id)
        {
            var cur = await _context.Feedback.FindAsync(id);
            if (cur != null)
            {
                _context.Feedback.Remove(cur);
                await _context.SaveChangesAsync();
            }
        }

        public async  Task<List<FeedbackDto>> GetUserFeedback(int id)
        {
            var users = await _context.Feedback
                .Where(i => i.User.Id == id)
                .Include(e => e.User)
                .Select(i => new FeedbackDto()
                {

                    Id = i.Id,
                    Course = i.Course,
                    Date = i.Date,
                    Name = i.Name,
                    User = i.User,
                    Phone_Number = i.Phone_Number,
                }).ToListAsync();
            return users;
        }

        public async Task<List<FeedbackDto>> GetAllFeetback()
        {
            var les = await _context.Feedback
                .Include(i => i.Course)
                .Include(i => i.User)
                .Select(i => new FeedbackDto()
                {
                    Course = i.Course,
                    User = i.User,
                    Id = i.Id,
                    Name = i.Name,
                    Date = i.Date,
                    Phone_Number = i.Phone_Number,
                })
                .ToListAsync();
            return les;
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            var leso = await _context.Feedback
                .Include(i => i.Course)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var lesson = new FeedbackDto();
            lesson.Id = id;
            lesson.Course = leso.Course;
            lesson.Date = leso.Date;
            lesson.Name = leso.Name;
            lesson.Phone_Number = leso.Phone_Number;
            lesson.User = leso.User;
            return leso;
        }
    }
}
