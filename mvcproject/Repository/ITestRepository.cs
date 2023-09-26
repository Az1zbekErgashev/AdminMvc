using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public interface ITestRepository
    {
        Task<List<TestsDto>> GetAllTest();
        Task<Tests> GetTestById(int id);
        Task CreateTest(Tests test);
        Task UpdateTest(int id, TestsDto testDto);
        Task DeleteTest(int id);
    }
}