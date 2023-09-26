using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository;

public class LessonRepository : ILessonsRepository
{
    private readonly AppDbContext _context;
    public LessonRepository(AppDbContext context) => _context = context;
    public async System.Threading.Tasks.Task CreateLesson(Lessons lessons)
    {
        _context.Lessons.Add(lessons);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteLessonsById(int id)
    {
        var cur = await _context.Lessons.FindAsync(id);
        if (cur != null)
        {
            _context.Lessons.Remove(cur);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<LessonsDto>> GetAllLessons()
    {
        var les = await _context.Lessons
            .Include(i => i.Course)
            .Select(i => new LessonsDto()
            {
                Course = i.Course,
                Description = i.Description,
                Title = i.Title,
                Vedeo_url2 = i.Vedeo_url2,
                Video_url = i.Video_url,
                Id = i.Id,
            })
            .ToListAsync();
        return les;
    }

    public async Task<Lessons> GetLessonsById(int id)
    {
        var leso = await _context.Lessons
            .Include(i => i.Course)
            .FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var lesson = new LessonsDto();
        lesson.Id = id;
        lesson.Course = leso.Course;
        lesson.Description = leso.Description;
        lesson.Title = leso.Title;
        lesson.Vedeo_url2 = leso.Vedeo_url2;
        lesson.Video_url = leso.Video_url;
        return leso;

    }

    public async System.Threading.Tasks.Task UpdateLessons(int id, LessonsDto lessonsDto)
    {
        var cur = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        if (cur != null)
        {
            cur.Title = lessonsDto.Title;
            cur.Video_url = lessonsDto.Video_url;
            cur.Description = lessonsDto.Description;
            cur.Vedeo_url2 = lessonsDto.Vedeo_url2;
            cur.Course = lessonsDto.Course;


            _context.Entry(cur).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
