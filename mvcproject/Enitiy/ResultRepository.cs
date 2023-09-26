using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Repository;

namespace mvcproject.Enitiy
{
    public class ResultRepository : IResultRepository
    {

        private readonly AppDbContext _context;
        public ResultRepository(AppDbContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task CreateResult(Result result)
        {
            _context.Result.Add(result);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteResultById(int id)
        {
            var cur = await _context.Result.FindAsync(id);
            if (cur != null)
            {
                _context.Result.Remove(cur);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ResultDto>> GetUserResult(int id)
        {
            var results = await _context.Result
                .Where(i => i.User.Id == id)
                .Include(e => e.Education)
                .Include(e => e.User)
                .Select(i => new ResultDto()
                {

                   ID = i.ID,
                   Name = i.Name,
                   User = i.User,
                   Education = i.Education,
                   Url = i.Url,


                }).ToListAsync();
            return results;
        }

        public async Task<List<ResultDto>> GetAllResults()
        {
            var les = await _context.Result
                .Include(i => i.Education)
                .Include(i => i.User)
                .Select(i => new ResultDto()
                {
                    Url = i.Url,
                    Education = i.Education,
                    Name = i.Name,
                    User = i.User,
                    ID = i.ID,
                })
                .ToListAsync();
            return les;
        }

        public async Task<Result> GetResultById(int id)
        {
            var leso = await _context.Result
                .Include(i => i.User)
                .Include(i => i.Education)
                .FirstOrDefaultAsync(i => i.ID == id) ?? throw new BadHttpRequestException("Not Found");
            var lesson = new ResultDto();
            lesson.ID = id;
            lesson.Url = leso.Url;
            lesson.Name = leso.Name;
            lesson.Education = leso.Education;
            lesson.User = leso.User;
            return leso;
        }

        public async System.Threading.Tasks.Task UpdateResult(int id, ResultDto resultDto)
        {
            var cur = await _context.Result.FirstOrDefaultAsync(i => i.ID == id);
            if (cur != null)
            {
                cur.Name = resultDto.Name;
                cur.Url = resultDto.Url;
                cur.Education = resultDto.Education;
                cur.User = resultDto.User;

                _context.Entry(cur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
