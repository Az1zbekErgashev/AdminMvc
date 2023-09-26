using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly AppDbContext _context;
        public TestRepository(AppDbContext context) => _context = context;
        public async Task CreateTest(Tests test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteTest(int id)
        {
            var CurTest = await _context.Tests.FindAsync(id);
            if (CurTest != null)
            {
                _context.Tests.Remove(CurTest);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<TestsDto>> GetAllTest()
        {
            var testAll = await _context.Tests
                .Include(e => e.Course)
                .Select(i => new TestsDto()
                {
                    Id = i.Id,
                    correct = i.correct,
                    incorrect = i.incorrect,
                    Course = i.Course,
                    Queshioquestion = i.Queshioquestion,
                })
                .ToListAsync();

            return testAll;
        }

        public async Task<Tests> GetTestById(int id)
        {
            var testAll = await _context.Tests

                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var tests = new TestsDto();
            tests.Id = id;
            tests.correct = testAll.correct;
            tests.incorrect = testAll.incorrect;
            tests.Queshioquestion = testAll.Queshioquestion;
            tests.Course = testAll.Course;
            return testAll;
        }

        public async Task UpdateTest(int id, TestsDto testDto)
        {
            var CurTest = await _context.Tests.FirstOrDefaultAsync(i => i.Id == id);

            if (CurTest != null)
            {
                CurTest.correct = testDto.correct;
                CurTest.incorrect = testDto.incorrect;
                CurTest.Queshioquestion = testDto.Queshioquestion;
                CurTest.Course = await _context.Course.FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("User not found");

                _context.Entry(CurTest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
