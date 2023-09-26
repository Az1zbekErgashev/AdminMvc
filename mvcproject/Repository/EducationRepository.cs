using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;


namespace mvcproject.Repository
{
    public class EducationRepository : IEducationRepository
    {
        private readonly AppDbContext _context;
        public EducationRepository(AppDbContext context) => _context = context;

        public async Task<List<EducationDto>> GetAllEducation()
        {
            var list = await _context.Education
                .Include(e => e.course)
                .Select(i => new EducationDto()
                {
                    Id = i.Id,
                    Tranding = i.Tranding,
                    Description = i.Description,
                    courseСompletion = i.courseСompletion,
                    Course = i.course,

                })
                .ToListAsync();
            return list;    

        }


        public async System.Threading.Tasks.Task CreateEducation(Education education)
        {
            _context.Education.Add(education);
            await _context.SaveChangesAsync();

        }
        public async Task<Education> GetEducationById(int id)
        {
            var list = await _context.Education
                .Include(education => education.course)
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var newlistArr = new EducationDto();
            newlistArr.Id = list.Id;
            newlistArr.Description = list.Description;
            newlistArr.Tranding = list.Description;
            newlistArr.courseСompletion = list.courseСompletion;
            newlistArr.Course = list.course;
            return list;

        }




        public async System.Threading.Tasks.Task DeleteEducationById(int id)
        {
            var CurEducation = await _context.Education.FindAsync(id);
            if (CurEducation != null)
            {
                _context.Education.Remove(CurEducation);
                await _context.SaveChangesAsync();

            }
        }



        public async System.Threading.Tasks.Task UpdateEducation(int id, EducationDto educationDto)
        {
            var CurEducation = await _context.Education.FirstOrDefaultAsync(i => i.Id == id);
            if (CurEducation != null)
            {
                CurEducation.course = educationDto.Course;
                CurEducation.Description = educationDto.Description;
                CurEducation.Tranding = educationDto.Tranding;
                CurEducation.courseСompletion = educationDto.courseСompletion;
                _context.Entry(CurEducation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
