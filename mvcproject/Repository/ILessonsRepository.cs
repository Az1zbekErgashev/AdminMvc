using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository;

public interface ILessonsRepository
{
    Task<List<LessonsDto>> GetAllLessons();
    Task<Lessons> GetLessonsById(int id);
    Task CreateLesson(Lessons lessons);
    Task UpdateLessons(int id, LessonsDto lessonsDto);
    Task DeleteLessonsById(int id);

}
