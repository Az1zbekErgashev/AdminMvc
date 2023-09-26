using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository;
public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context) => _context = context;


    public async Task<List<CourseDto>> GetCourseAllAsync()
    {
        var users = await _context.Course
            .Select(i => new CourseDto()
            {
                Url = i.Url, Name = i.Name, Id = i.Id, Description = i.Description, Price = i.Price
            })
            .ToListAsync();
        return users;

    }




    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var users = await _context.Course
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var userall = new CourseDto();
        userall.Url = users.Url;
        userall.Description = users.Description;
        userall.Name = users.Name;
        userall.Price = users.Price;

        return users;
    }



    public async Task CreateCourseAsync(Course course)
    {
         _context.Course.Add(course);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateCourseAsync(int id, CourseDto courseDto)
    {
        var coursee = await _context.Course.FirstOrDefaultAsync(i => i.Id == id);
        if (coursee != null)
        {
            coursee.Name = courseDto.Name;
            coursee.Url = courseDto.Url;
            coursee.Price = courseDto.Price;
            coursee.Description = courseDto.Description;
            _context.Entry(coursee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteCourseAsync(int id)
    {

        var course = await _context.Course.FindAsync(id);
        if (course != null)
        {
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
        }
    }

}