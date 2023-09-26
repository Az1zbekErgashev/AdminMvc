using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public interface ICourseRepository
    {
        Task<List<CourseDto>> GetCourseAllAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task CreateCourseAsync(Course course);
        Task UpdateCourseAsync(int id, CourseDto courseDto);
        Task DeleteCourseAsync(int id);

    }
}