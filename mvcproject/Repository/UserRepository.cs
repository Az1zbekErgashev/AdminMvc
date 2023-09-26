using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;


namespace mvcproject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;
        public async System.Threading.Tasks.Task CreateUsers(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteUsers(int id)
        {
            var CurUser = await _context.User.FindAsync(id);
            if (CurUser != null)
            {
                _context.User.Remove(CurUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _context.User
                .Select(i => new UserDto()
                {
                    Email = i.Email,
                    FullName = i.FullName,
                    Id = i.Id,
                    Password = i.Password,
                })
                .ToListAsync();
            return users;

        }

        public async Task<User> GetById(int id)
        {
            var users = await _context.User

                 .Include(i => i.Course)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var userall = new UserDto();
            userall.FullName = users.FullName;
            userall.Email = users.Email;
            userall.Password = users.Password;


            return users;

        }

        public async Task<User> GetUserByEmail(string email) => await _context.User.FirstOrDefaultAsync(e => e.Email == email) ?? throw new BadHttpRequestException("User not found");


        public async Task<List<CourseDto>> GetUserCourse(int id)
        {
            var users = await _context.User
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("User Not found");
            var course = users.Course.Select(cour => new CourseDto()
            {
                Url = cour.Url,
                Description = cour.Description,
                Name = cour.Name,
                Id = cour.Id,
                Price = cour.Price,

            }).ToList();

            return course;

        }


        public async System.Threading.Tasks.Task UpdateUsers(int id, UserDto userDto)
        {
            var sss = await _context.User.FirstOrDefaultAsync(i => i.Id == id);
            if (sss != null)
            {
               
                sss.Email = userDto.Email;
                sss.FullName = userDto.FullName;
                sss.Password = userDto.Password;

                _context.Entry(sss).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
        }
    }

}
