using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;


namespace mvcproject.Repository
{
    public class HomeWorkRepository : IHomeWorkRepository
    {
        private readonly AppDbContext _context;
        public HomeWorkRepository(AppDbContext context) => _context = context;

        public async System.Threading.Tasks.Task CreateHome(HomeWork homeWork)
        {
            _context.HomeWork.Add(homeWork);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteHomeWorkById(int id)
        {
            var CurEducation = await _context.HomeWork.FindAsync(id);
            if (CurEducation != null)
            {
                _context.HomeWork.Remove(CurEducation);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<HomeWorkDto>> GetAllHomeWork()
        {
            var les = await _context.HomeWork
                .Include(i => i.Task)
                .Select(i => new HomeWorkDto()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Title = i.Title,
                    Img = i.Img,
                    Task = i.Task,
                })
                .ToListAsync();
            return les;
        }

        public async Task<HomeWork> GetHomeWorkById(int id)
        {
            var leso = await _context.HomeWork
                .Include(i => i.Task)
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var lesson = new HomeWorkDto();
            lesson.Id = id;
            lesson.Img = leso.Img;
            lesson.Task = leso.Task;
            lesson.Title = leso.Title;
            lesson.Name = leso.Name;

            return leso;
        }

        public async System.Threading.Tasks.Task UpdateHomeWork(int id, HomeWorkDto homeWorkDto)
        {
            var CurExcerpt = await _context.HomeWork.FirstOrDefaultAsync(i => i.Id == id);

            if (CurExcerpt != null)
            {
                CurExcerpt.Img = homeWorkDto.Img;
                CurExcerpt.Title = homeWorkDto.Title;
                CurExcerpt.Name = homeWorkDto.Name;
                CurExcerpt.Task = homeWorkDto.Task;

                _context.Entry(CurExcerpt).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
