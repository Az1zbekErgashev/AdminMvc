using mvcproject.Dto;
using mvcproject.Enitiy;

namespace mvcproject.Repository
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Task<User> GetById(int id);
        System.Threading.Tasks.Task CreateUsers(User user);
        System.Threading.Tasks.Task UpdateUsers(int id, UserDto userDto);
        System.Threading.Tasks.Task DeleteUsers(int id);
        Task<User> GetUserByEmail(string email);
        Task<List<CourseDto>> GetUserCourse(int id);
    }
}