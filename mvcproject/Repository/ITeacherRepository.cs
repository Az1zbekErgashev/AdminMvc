using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository;

public interface ITeacherRepository
{
    Task<List<TeacherDto>> GetAllTeachers();
    Task<Teacher> GetTeacherById(int id);
    Task CreateTecher(Teacher teacher);
    Task UpdateTeacher(int id, TeacherDto teacherDto);
    Task DeleteTeacherById(int id);
}
