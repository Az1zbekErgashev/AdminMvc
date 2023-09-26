using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;

namespace mvcproject.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository(AppDbContext context) => _context = context;

        public async System.Threading.Tasks.Task CreateTecher(Teacher teacher)
        {
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTeacherById(int id)
        {
            var CurTeacher = await _context.Teacher.FindAsync(id);
            if (CurTeacher != null)
            {
                _context.Teacher.Remove(CurTeacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TeacherDto>> GetAllTeachers()
        {
            var list = await _context.Teacher
                .Select(i => new TeacherDto()
                {
                    Id = i.Id,
                    Url = i.Url,
                    Description = i.Description,
                    Name = i.Name,
                })
                .ToListAsync();
            return list;
        }
        
        public async Task<Teacher> GetTeacherById(int id)
        {
            var users = await _context.Teacher
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var userall = new TeacherDto();
            userall.Id = id;
            userall.Url = users.Url;
            userall.Name = users.Name;
            userall.Description = users.Description;


            return users;
        }

        public async System.Threading.Tasks.Task UpdateTeacher(int id, TeacherDto teacherDto)
        {
            var CurTeacher = await _context.Teacher.FirstOrDefaultAsync(i => i.Id == id);
            if (CurTeacher != null)
            {
                CurTeacher.Name = teacherDto.Name;
                CurTeacher.Url = teacherDto.Url;
                CurTeacher.Description = teacherDto.Description;

                _context.Entry(CurTeacher).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
